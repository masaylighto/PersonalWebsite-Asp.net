namespace Core.LinqExtensions;

public static class IEnumerableExtension
{
    public static void Apply<Item>(this IEnumerable<Item> source, Action<Item> WhatToDO)
    {
        foreach (var item in source)
        {
            WhatToDO(item);
        }
    }
    public static IEnumerable<Item> Page<Item>(this IEnumerable<Item> source, int pageSize, int pageNumber = 0)
    {
        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
    public static IQueryable<Item> Page<Item>(this IQueryable<Item> source, int pageSize, int pageNumber = 0)
    {
        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
