using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Utilities
{
    private static System.Random rng = new System.Random();

    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static bool IsWalkable(Vector2 position)
    {
        bool isWalkable = true;
        Collider2D[] c2d = Physics2D.OverlapPointAll(position);
        foreach (Collider2D c in c2d)
        {
            Tile t = c.gameObject.GetComponent<Tile>();
            if (t != null)
            {
                if (!t.isWalkable) isWalkable = false;
            }
            else
            {
                isWalkable = false;
            }
        }
        return isWalkable;
    }
}