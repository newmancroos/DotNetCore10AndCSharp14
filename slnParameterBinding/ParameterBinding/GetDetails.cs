namespace ParameterBinding;

public interface IGetDetails
{
    string GetName();
}

public class GetDetails : IGetDetails
{
    public string GetName()
    {
        return "Newman Croos";
    }
}