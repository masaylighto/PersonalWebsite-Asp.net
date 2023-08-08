

using Core.DataKit.Exceptions;
using Core.DataKit.Result;

namespace Core.DataKit.Wrapper;

public class Frozen<T>
{
    T Data { get; set; }
    public Frozen(T data)
    {
        Data = data;

    }
    bool IsFrozen { get; set; }
    public Result<OK> Set(T data)
    {
        if (IsFrozen)
        {
            return new ObjectIsFrozenException();
        }
        if (data == null)
        {
            return new ArgumentNullException("data is null");
        }
        this.Data = data;
        return new OK();
    }

    public static implicit operator Result<T>(Frozen<T> freeze)
    {
        return freeze.Data;
    }
    public static implicit operator Frozen<T>(T data)
    {
        return new Frozen<T>(data);
    }
    public void Freeze()
    {
        IsFrozen = true;
    }
    public void UnFreeze()
    {
        IsFrozen = true;
    }
}
