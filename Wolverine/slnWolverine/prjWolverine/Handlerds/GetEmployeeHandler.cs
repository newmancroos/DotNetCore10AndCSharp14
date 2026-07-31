using Wolverine;

namespace prjWolverine.Handlerds;

public class GetEmployeeHandler(IMessageBus bus)
{
    public async Task<GetEmployeeResponse> Handle(GetEmployeeRequest request)
    {
        var employee= new GetEmployeeResponse(1, "Newman", DateTime.Parse("06/19/1973"));

        await bus.PublishAsync(employee);
        return employee;
    }
}
