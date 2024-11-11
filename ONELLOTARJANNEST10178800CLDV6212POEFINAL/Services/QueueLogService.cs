using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace ONELLOTARJANNEST10178800CLDV6212POEFINAL.Services
{
    public class QueueLogService
    {
        private readonly string _connectionString;

        public QueueLogService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }

        public async Task LogOrderAsync(string orderId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO QueueLogs (OrderId) VALUES (@OrderId)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@OrderId", orderId);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
