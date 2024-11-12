using Azure;
using Azure.Data.Tables;
using Microsoft.Extensions.Configuration;
using ONELLOTARJANNEST10178800CLDV6212POEFINAL.Models;
using System.Threading.Tasks;

namespace ONELLOTARJANNEST10178800CLDV6212POEFINAL.Services
{
    public class TableService
    {
        private readonly TableServiceClient _serviceClient;
        private readonly TableClient _userProfilesTableClient;
        private readonly TableClient _productsTableClient;
        private readonly CustomerTblService _customerTblService; // SQL Server service for customers
        private readonly ProductsTblService _productsTblService; // SQL Server service for products

        public TableService(IConfiguration configuration, CustomerTblService customerTblService, ProductsTblService productsTblService)
        {
            _customerTblService = customerTblService; 
            _productsTblService = productsTblService; 

            var connectionString = configuration["AzureStorage:ConnectionString"];
            _serviceClient = new TableServiceClient(connectionString);

            _userProfilesTableClient = _serviceClient.GetTableClient("UserProfiles");
            _userProfilesTableClient.CreateIfNotExists();

            _productsTableClient = _serviceClient.GetTableClient("Products");
            _productsTableClient.CreateIfNotExists();
        }

        public async Task AddUserProfileAsync(UserProfile profile)
        {
            await _userProfilesTableClient.AddEntityAsync(profile);

            
            await _customerTblService.AddCustomerAsync(new UserProfile
            {
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Email = profile.Email,
                PhoneNumber = profile.PhoneNumber
            });
        }

        public async Task AddProductAsync(Product product)
        {
            
            await _productsTableClient.AddEntityAsync(product);

         
            await _productsTblService.AddProductAsync(new Product
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Price = product.Price,
                Quantity = product.Quantity
            });
        }
    }
}
