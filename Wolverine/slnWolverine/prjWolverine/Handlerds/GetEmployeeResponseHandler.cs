namespace prjWolverine.Handlerds;

public class GetEmployeeResponseHandler
{
    //This will consume the message from RabbitMq
    public async Task Handle(GetEmployeeResponse input)
    {
        var EmployeeResult = input;

    }
}
