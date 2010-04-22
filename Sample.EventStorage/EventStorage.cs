using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Sample.Utilities;

namespace Sample.EventStorage
{
    public class EventStorage : IEventStorage
    {
        readonly IEventSerializer _serializer;
        readonly IDatabaseConfig _dbConfig;

        public EventStorage(IEventSerializer serializer, IDatabaseConfig dbConfig)
        {
            _serializer = serializer;
            _dbConfig = dbConfig;
        }

        public void Save(IEventProvider eventProvider)
        {
            Within.Transaction(_dbConfig.ConnectionString, transaction =>
            {
                int version = GetEventProviderVersion(eventProvider.Id, transaction);
                if (version == -1)
                    StoreEventProvider(eventProvider, transaction);

                if (version > eventProvider.Version)
                    throw new ConcurrencyViolationException();

                eventProvider.GetChanges().ForEach(e => StoreChange(e, eventProvider.Id, transaction));

                UpdateEventProviderVersion(eventProvider, transaction);
            });
        }

        public IEnumerable<IEvent> GetAllEventsForEventProvider(Guid id)
        {
            return GetEventsFromVersionForEventProvider(id, 0);
        }

        public IEnumerable<IEvent> GetEventsFromVersionForEventProvider(Guid id, int version)
        {
            var events = new List<IEvent>();

            Within.Transaction(_dbConfig.ConnectionString, transaction =>
            {
                const string commandText = "SELECT Data FROM Event WHERE EventProvider_Id = @eventProviderId AND Version > @version";
                using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
                {
                    command.Parameters.Add(new SqlParameter("@eventProviderId", id));
                    command.Parameters.Add(new SqlParameter("@version", version));

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader != null && reader.Read())
                        {
                            events.Add(_serializer.Deserialize((byte[])reader["Data"]));
                        }
                    }
                }
            });
            return events;
        }

        static void StoreEventProvider(IEventProvider eventProvider, SqlTransaction transaction)
        {
            const string commandText = "INSERT INTO EventProvider(EventProviderId, Type) VALUES(@eventProviderId, @type)";

            using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@eventProviderId", eventProvider.Id));
                command.Parameters.Add(new SqlParameter("@type", eventProvider.GetType().FullName));

                command.ExecuteNonQuery();
            }
        }

        static void UpdateEventProviderVersion(IEventProvider eventProvider, SqlTransaction transaction)
        {
            int numberOfChanges = eventProvider.GetChanges().Count();

            const string commandText = "UPDATE EventProvider SET Version = Version + @numberOfChanges WHERE EventProviderId = @eventProviderId";
            using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@eventProviderId", eventProvider.Id));
                command.Parameters.Add(new SqlParameter("@numberOfChanges", numberOfChanges));

                command.ExecuteNonQuery();
            }
        }

        static int GetEventProviderVersion(Guid eventProviderId, SqlTransaction transaction)
        {
            const string commandText = "SELECT Version FROM EventProvider WHERE EventProviderId = @eventProviderId";
            using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@eventProviderId", eventProviderId));

                using (var reader = command.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        return (int)reader["Version"];
                    }
                }
            }

            return -1;
        }

        void StoreChange(IEvent change, Guid eventProviderId, SqlTransaction transaction)
        {
            byte[] serializedChange = _serializer.Serialize(change);
            int version = GetNextVersionNumber(eventProviderId, transaction);

            const string commandText = "INSERT INTO Event VALUES(@eventProviderId, @type, @data, @dateTime, @version)";
            using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@eventProviderId", eventProviderId));
                command.Parameters.Add(new SqlParameter("@type", change.GetType().FullName));
                command.Parameters.Add(new SqlParameter("@data", serializedChange));
                command.Parameters.Add(new SqlParameter("@dateTime", DateTime.Now));
                command.Parameters.Add(new SqlParameter("@version", version));

                command.ExecuteNonQuery();
            }
        }

        static int GetNextVersionNumber(Guid eventProviderId, SqlTransaction transaction)
        {
            const string commandText = "SELECT ISNULL(MAX(Version), 0) AS MaxVersion FROM Event WHERE EventProvider_Id = @eventProviderId";
            using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
            {
                command.Parameters.Add(new SqlParameter("@eventProviderId", eventProviderId));

                using (var reader = command.ExecuteReader())
                {
                    while (reader != null && reader.Read())
                    {
                        return (int)reader["MaxVersion"] + 1;
                    }
                }
            }

            return 1;
        }
    }
}