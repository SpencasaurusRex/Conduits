using System;
using UnityEngine;

namespace Conduits.Core
{
    [System.Serializable]
    public struct Level
    {
        [SerializeField]
        public Tile[,] Tiles;
        [SerializeField]
        public int[] inventory;
    }
}
