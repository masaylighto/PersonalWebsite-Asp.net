

using Core.DataKit.Result;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Core.DataKit.ReturnWrapper
{
    public class State
    {
        Result<OK> Result { get; set; }
        public State(OK oK) 
        {
            Result = oK;
        }
        public State(Exception error)
        {
            Result = error;
        }
        public bool IsOk() => Result.ContainData();
       
        public bool IsNotOk() => Result.ContainError();
       
        public Exception GetError() => Result.GetError();

        public string GetErrorMessage() => Result.GetErrorMessage();
        public bool IsInternalError() => Result.IsInternalError();

        public bool IsErrorType<T>() => Result.IsErrorType<T>();

        public static implicit operator State(OK ok) => new State(ok);
        
        public static implicit operator State(Exception error) => new State(error);
        public static State operator +(State a, Exception b)
        {
            return new AggregateException(new Exception[] {
                a.GetError(),
                b
            });
           
        }

    }
}
