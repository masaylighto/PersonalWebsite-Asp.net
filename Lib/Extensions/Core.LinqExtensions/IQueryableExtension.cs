namespace Core.LinqExtensions;

public static class IQueryableExtension
{
    public static async IAsyncEnumerable<TResult> SelectAsync<TSource, TResult>(this IQueryable<TSource> source, Func<TSource, ValueTask<TResult>> selector)
    {
        foreach (var item in source)
        {
           yield return await selector(item);
        }
    }
    public static async IAsyncEnumerable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Func<TSource, TResult> selector)
    {
        foreach (var item in source)
        {
            yield return await Task.Run(()=>selector(item));
        }
    }

    public static IQueryable<Entity> Page<Entity>(this IQueryable<Entity> source, int pageSize, int pageNumber = 0)
    {
        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}