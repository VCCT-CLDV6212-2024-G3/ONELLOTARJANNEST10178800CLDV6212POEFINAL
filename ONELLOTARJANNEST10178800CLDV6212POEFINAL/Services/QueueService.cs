using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;


namespace ONELLOTARJANNEST10178800CLDV6212POEFINAL.Services
{
    public class QueueService
    {
        private readonly QueueServiceClient _queueServiceClient;

        public QueueService(IConfiguration configuration)
        {
            _queueServiceClient = new QueueServiceClient(configuration["AzureStorage:ConnectionString"]);
        }

        public async Task SendMessageAsync(string queueName, string message)
        {
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            await queueClient.CreateIfNotExistsAsync();
            await queueClient.SendMessageAsync(message);
        }
        public async Task<IEnumerable<string>> ReceiveMessagesAsync(string queueName, int maxMessages = 10)
        {
            var queueClient = _queueServiceClient.GetQueueClient(queueName);
            var messages = await queueClient.ReceiveMessagesAsync(maxMessages);

            List<string> messageContents = new List<string>();
            foreach (var message in messages.Value)
            {
                messageContents.Add(message.MessageText);
            }

            return messageContents;
        }

    }

}
