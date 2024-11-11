using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using ONELLOTARJANNEST10178800CLDV6212POEFINAL.Models;

namespace ONELLOTARJANNEST10178800CLDV6212POEFINAL.Services
{
    public class ProductsTblService
    {
        private readonly string _connectionString;

        public ProductsTblService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("SqlServerConnection");
        }

        public async Task AddProductAsync(ProductsTbl product)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var query = "INSERT INTO ProductsTbl (ProductID, ProductName, Price, Quantity) VALUES (@ProductID, @ProductName, @Price, @Quantity)";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductID", product.ProductID);
                    command.Parameters.AddWithValue("@ProductName", product.ProductName);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
