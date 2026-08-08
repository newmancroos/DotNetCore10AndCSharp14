using Microsoft.AspNetCore.Mvc;
using System.Threading.Channels;

namespace Channel_Producer_Consumer.Controllers;

[ApiController]
[Route("api/tasks")]
public class ChannelWriterController:ControllerBase
{
    private readonly ChannelWriter<WorkTask> _channelWriter;

    public ChannelWriterController(Channel<WorkTask> channelWriter)
    {
        _channelWriter = channelWriter.Writer;
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] string payload)
    {
        var newTask = new WorkTask(Guid.NewGuid(), payload, DateTime.UtcNow);

        // Asynchronously hands work over to the background pipeline and frees the HTTP thread
        await _channelWriter.WriteAsync(newTask);

        return Accepted(new { TaskId = newTask.Id, Status = "Queued in Background" });
    }
}
