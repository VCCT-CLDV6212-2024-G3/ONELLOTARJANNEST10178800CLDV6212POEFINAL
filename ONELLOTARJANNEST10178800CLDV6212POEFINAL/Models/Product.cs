using Azure;
using Azure.Data.Tables;
using System;

namespace ONELLOTARJANNEST10178800CLDV6212POEFINAL.Models
{
    public class Product : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string ProductID { get; set; }
        public string ProductName { get; set; }
        public string Price { get; set; }
        public string Quantity { get; set; }

        public Product()
        {
            PartitionKey = "Product";
            RowKey = Guid.NewGuid().ToString();
        }
    }
}
