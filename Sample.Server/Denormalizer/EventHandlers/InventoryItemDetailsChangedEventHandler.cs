using System.Data.SqlClient;
using Sample.Events;
using Sample.EventStorage;
using Sample.Utilities;

namespace Sample.Server.Denormalizer.EventHandlers
{
    public class InventoryItemDetailsChangedEventHandler : EventHandler<InventoryItemDetailsChangedEvent>
    {
        readonly IDatabaseConfig _dbConfig;

        public InventoryItemDetailsChangedEventHandler(IDatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public override void Handle(InventoryItemDetailsChangedEvent e)
        {
            Within.Transaction(_dbConfig.ConnectionString, transaction =>
            {
                const string commandText = "UPDATE InventoryItems SET Name = @name, Description = @description, Version = Version + 1 WHERE AggregateId = @aggregateId";
                using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
                {
                    command.Parameters.Add(new SqlParameter("@aggregateId", e.AggregateId));
                    command.Parameters.Add(new SqlParameter("@name", e.Name));
                    command.Parameters.Add(new SqlParameter("@description", e.Description));
                    command.ExecuteNonQuery();
                }
            });
        }
    }
}