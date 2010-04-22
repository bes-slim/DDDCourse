using System.Data.SqlClient;
using Sample.Events;
using Sample.EventStorage;
using Sample.Utilities;

namespace Sample.Server.Denormalizer.EventHandlers
{
    public class InventoryItemActivatedEventHandler : EventHandler<InventoryItemActivatedEvent>
    {
        readonly IDatabaseConfig _dbConfig;

        public InventoryItemActivatedEventHandler(IDatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public override void Handle(InventoryItemActivatedEvent e)
        {
            Within.Transaction(_dbConfig.ConnectionString, transaction =>
            {
                const string commandText = "UPDATE InventoryItems SET IsActive = 1, Version = Version + 1 WHERE AggregateId = @aggregateId";
                using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
                {
                    command.Parameters.Add(new SqlParameter("@aggregateId", e.AggregateId));
                    command.ExecuteNonQuery();
                }
            });
        }
    }
}