#pragma warning disable CS8618 // Non-nullable field warning. data in this class has ErrorExist to express if they are null

namespace Core.DataKit.Result;
public class Result<DataType>
{
    //constructer 
    public Result(Exception error)
    {
        Error = error;
        IsError = true;
    }

    public Result(DataType data)
    {
        Data = data;
        IsError = false;
    }

    protected Result()
    {

    }
    //Fields
    protected DataType Data { get; set; }
    protected Exception Error { get; set; }
    protected bool IsError { get; set; }
    //Polymorphism Method    
    public bool IsErrorOfType<Type>() => Error is Type;
    public bool ContainError() => IsError;
    public bool ContainData() => Data is not null;
    public DataType GetData() => Data;
    public Exception GetError() => Error;
    public void SetData(DataType? data)
    {
        if (data is null)
        {
            SetError(new ArgumentNullException(nameof(data), $"{nameof(Result)} Received null data"));
            return;
        }
        Data = data;
        IsError = false;
    }

    public virtual void SetError(Exception error)
    {
        Error = error;
        IsError = true;
    }


    //Implict Conversation  
    public static implicit operator Result<DataType>(DataType data)
    {
        Result<DataType> result = new();
        result.SetData(data);
        return result;
    }

    public static implicit operator Result<DataType>(Exception error)
    {
        Result<DataType> result = new();
        result.SetError(error);
        return result;
    }


}