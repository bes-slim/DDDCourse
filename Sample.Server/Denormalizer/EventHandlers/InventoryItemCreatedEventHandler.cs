using System.Data.SqlClient;
using Sample.Events;
using Sample.EventStorage;
using Sample.Utilities;

namespace Sample.Server.Denormalizer.EventHandlers
{
    public class InventoryItemCreatedEventHandler : EventHandler<InventoryItemCreatedEvent>
    {
        readonly IDatabaseConfig _dbConfig;

        public InventoryItemCreatedEventHandler(IDatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public override void Handle(InventoryItemCreatedEvent e)
        {
            Within.Transaction(_dbConfig.ConnectionString, transaction =>
            {
                const string commandText = "INSERT INTO InventoryItems VALUES(@aggregateId, @name, @description, @count, @isActive, @version)";
                using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
                {
                    command.Parameters.Add(new SqlParameter("@aggregateId", e.AggregateId));
                    command.Parameters.Add(new SqlParameter("@name", e.Name));
                    command.Parameters.Add(new SqlParameter("@description", e.Description));
                    command.Parameters.Add(new SqlParameter("@count", e.Count));
                    command.Parameters.Add(new SqlParameter("@isActive", e.IsActive));
                    command.Parameters.Add(new SqlParameter("@version", 1));
                    command.ExecuteNonQuery();
                }
            });
        }
    }
}