using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    private static readonly System.Random random = new();

    public static T RandomListItem<T>(List<T> lst)
    {
        int idx = random.Next(0, lst.Count);
        return lst[idx];
    }
}
