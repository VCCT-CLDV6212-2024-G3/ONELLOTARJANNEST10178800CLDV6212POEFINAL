using Azure;
using Azure.Data.Tables;
using System;

namespace ONELLOTARJANNEST10178800CLDV6212POEFINAL.Models
{
    public class UserProfile : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public UserProfile()
        {
            PartitionKey = "UserProfile";
            RowKey = Guid.NewGuid().ToString();
        }
    }
}

