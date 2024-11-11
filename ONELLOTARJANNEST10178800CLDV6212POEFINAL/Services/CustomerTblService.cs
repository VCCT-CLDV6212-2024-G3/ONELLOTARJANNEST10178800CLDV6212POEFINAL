using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ONELLOTARJANNEST10178800CLDV6212POEFINAL.Models;

namespace ONELLOTARJANNEST10178800CLDV6212POEFINAL.Services
{
    public class CustomerTblService
    {
        private readonly string _connectionString;

        public CustomerTblService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }

        public async Task AddCustomerAsync(CustomerTbl customer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO CustomerTbl (FirstName, LastName, Email, PhoneNumber) VALUES (@FirstName, @LastName, @Email, @PhoneNumber)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", customer.FirstName);
                    command.Parameters.AddWithValue("@LastName", customer.LastName);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
