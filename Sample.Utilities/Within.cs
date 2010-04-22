using System;
using System.Data.SqlClient;

namespace Sample.Utilities
{
    public static class Within
    {
        public static void Transaction(string connectionString, Action<SqlTransaction> action)
        {
            Connection(connectionString, connection =>
            {
                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action(transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            });
        }

        public static void Connection(string connectionString, Action<SqlConnection> action)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                action(connection);
                connection.Close();
            }
        }
    }
}