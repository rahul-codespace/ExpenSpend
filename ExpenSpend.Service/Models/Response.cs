namespace ExpenSpend.Service.Models;

public class Response
{
    public bool IsSuccess { get; }
    public object Data { get; }

    public Response(object data)
    {
        IsSuccess = true;
        Data = data;
    }

    public Response(string errorMessage)
    {
        IsSuccess = false;
        Data = errorMessage;
    }
}
