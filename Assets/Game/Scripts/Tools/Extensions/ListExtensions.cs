﻿using System.Collections.Generic;
using UnityEngine;

namespace HCPJ3.Tools.Extensions
{
    public static class IListExtensions
    {
        public static void Shuffle<T>(this IList<T> ts)
        {
            int count = ts.Count;
            int last = count - 1;
        
            for (int i = 0; i < last; ++i)
            {
                int r = Random.Range(i, count);
                T tmp = ts[i];
                ts[i] = ts[r];
                ts[r] = tmp;
            }
        }
    }
}