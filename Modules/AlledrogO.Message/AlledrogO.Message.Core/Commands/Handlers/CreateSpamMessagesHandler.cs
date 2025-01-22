using System.Text.Json;
using AlledrogO.Shared.Commands;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class CreateSpamMessagesHandler : ICommandHandler<CreateSpamMessages>
{
    private readonly IAmazonSQS _sqsClient;
    private readonly string _queueUrl;
    
    public CreateSpamMessagesHandler(IAmazonSQS sqsClient)
    {
        _sqsClient = sqsClient;
        _queueUrl = Environment.GetEnvironmentVariable("SQS_QUEUE_URL")
                    ?? throw new NullReferenceException("SQS_QUEUE_URL environment variable is not set");
    }

    public async Task HandleAsync(CreateSpamMessages command)
    {
        var messageBatch = new List<SendMessageBatchRequestEntry>();
        for (int i = 0; i < command.Count; i++)
        {
            var message = new Entities.Message()
            {
                CreatedAt = DateTime.Now,
                Content = "This is a spam message"
            };
            
            var messageJson = JsonSerializer.Serialize(message);
            var messageEntry = new SendMessageBatchRequestEntry()
            {
                Id = Guid.NewGuid().ToString(),
                MessageBody = messageJson
            };
            messageBatch.Add(messageEntry);
            if (messageBatch.Count == 10)
            {
                await _sqsClient.SendMessageBatchAsync(_queueUrl, messageBatch);
                messageBatch.Clear();
            }
        }
        if (messageBatch.Count > 0)
        {
            await _sqsClient.SendMessageBatchAsync(_queueUrl, messageBatch);
        }
    }
}