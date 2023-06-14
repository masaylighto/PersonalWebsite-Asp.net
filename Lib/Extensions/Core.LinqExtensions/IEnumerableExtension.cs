namespace Core.LinqExtensions;

public static class IEnumerableExtension
{
    public static void Apply<Item>(this IEnumerable<Item> self, Action<Item> WhatToDO)
    {
        foreach (var item in self)
        {
             WhatToDO(item);
        }
    }
}