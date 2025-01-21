using System.Text.Json;
using AlledrogO.Shared.Commands;
using Amazon.SQS;

namespace AlledrogO.Message.Core.Commands.Handlers;

public class AddTestSqsMessageHandler : ICommandHandler<AddTestSqsMessage>
{
    private readonly IAmazonSQS _sqsClient;
    private readonly string _queueUrl;

    public AddTestSqsMessageHandler(IAmazonSQS sqsClient)
    {
        _sqsClient = sqsClient;
        _queueUrl = Environment.GetEnvironmentVariable("SQS_QUEUE_URL")
                    ?? throw new NullReferenceException("SQS_QUEUE_URL environment variable is not set");
    }

    public async Task HandleAsync(AddTestSqsMessage command)
    {
        var message = new Entities.Message()
        {
            CreatedAt = DateTime.Now,
            Content = "This is a test message"
        };
        
        var messageJson = JsonSerializer.Serialize(message);
        await _sqsClient.SendMessageAsync(_queueUrl, messageJson);
    }
}