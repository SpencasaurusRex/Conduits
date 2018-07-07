using System;
using UnityEngine;
namespace Conduits.Core
{
    public static class Util
    {
        public static Vector2Int Negate(this Vector2Int a)
        {
            return new Vector2Int(-a.x, -a.y);
        }
    }
}
