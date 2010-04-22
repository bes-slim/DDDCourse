using System.Data.SqlClient;
using Sample.Events;
using Sample.EventStorage;
using Sample.Utilities;

namespace Sample.Server.Denormalizer.EventHandlers
{
    public class InventoryItemCountAdjustedEventHandler : EventHandler<InventoryItemCountAdjustedEvent>
    {
        readonly IDatabaseConfig _dbConfig;

        public InventoryItemCountAdjustedEventHandler(IDatabaseConfig dbConfig)
        {
            _dbConfig = dbConfig;
        }

        public override void Handle(InventoryItemCountAdjustedEvent e)
        {
            Within.Transaction(_dbConfig.ConnectionString, transaction =>
            {
                const string commandText = "UPDATE InventoryItems SET Count = Count + @adjustedBy, Version = Version + 1 WHERE AggregateId = @aggregateId";
                using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
                {
                    command.Parameters.Add(new SqlParameter("@aggregateId", e.AggregateId));
                    command.Parameters.Add(new SqlParameter("@adjustedBy", e.AdjustedBy));
                    command.ExecuteNonQuery();
                }
            });
        }
    }
}