using System.Threading.Channels;

namespace Channel_Producer_Consumer;

public class ChannelConsumerService: BackgroundService
{
    private readonly ChannelReader<WorkTask> _channelReader; 
    private readonly ILogger<ChannelConsumerService> _logger;

    public ChannelConsumerService(Channel<WorkTask> channel, ILogger<ChannelConsumerService> logger)
    {
        _channelReader = channel.Reader;;
        _logger = logger;
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("ChannelConsumerService is starting.");

        
		try
		{

			await foreach(var task in _channelReader.ReadAllAsync(stoppingToken))
			{
                _logger.LogInformation("Processing Task Id: {Id} - Payload: {Payload}", task.Id, task.Payload);
                // Simulate some processing time
                await Task.Delay(1000, stoppingToken);
            }
        }
		catch (OperationCanceledException)
		{

            _logger.LogWarning("BackgroundService Channel Consumer stopped due to Cancel request");
		}
    }

}
