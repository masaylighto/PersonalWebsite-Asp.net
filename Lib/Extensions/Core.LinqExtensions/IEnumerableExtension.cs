namespace Core.LinqExtensions;

public static class IEnumerableExtension
{
    public static void Apply<Entity>(this IEnumerable<Entity> source, Action<Entity> WhatToDo)
    {
        foreach (var item in source)
        {
            WhatToDo(item);
        }
    }
    public static IEnumerable<Entity> Page<Entity>(this IEnumerable<Entity> source, int pageSize, int pageNumber = 0)
    {
        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}
