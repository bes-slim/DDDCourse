using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Sample.InventoryService.DTO;
using Sample.Utilities;

namespace Sample.InventoryService
{
    public class InventoryService : IInventoryService
    {
        readonly string _connectionString;

        public InventoryService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<InventoryItemSummaryDTO> GetSummaries()
        {
            var dtos = new List<InventoryItemSummaryDTO>();
            Within.Transaction(_connectionString, transaction =>
            {
                const string commandText = "SELECT AggregateId, Name FROM InventoryItems";
                using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
                {

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader != null && reader.Read())
                        {
                            dtos.Add(new InventoryItemSummaryDTO
                                        {
                                            Id = (Guid)reader["AggregateId"],
                                            Name = (string)reader["Name"]
                                        });
                        }
                    }
                }
            });

            return dtos;
        }

        public InventoryItemDTO GetInventoryItem(Guid id)
        {
            InventoryItemDTO dto = null;
            Within.Transaction(_connectionString, transaction =>
            {
                const string commandText = "SELECT Name, Description, Count, IsActive, Version FROM InventoryItems WHERE AggregateId = @aggregateId";
                using (var command = new SqlCommand(commandText, transaction.Connection, transaction))
                {
                    command.Parameters.Add(new SqlParameter("@aggregateId", id));
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader != null && reader.Read())
                        {
                            dto = new InventoryItemDTO
                                      {
                                          Id = id,
                                          Version = (int)reader["Version"],
                                          Name = (string)reader["Name"],
                                          Description = (string)reader["Description"],
                                          Count = (int)reader["Count"],
                                          Active = (bool)reader["IsActive"],
                                      };
                        }
                    }
                }
            });

            return dto;
        }
    }
}