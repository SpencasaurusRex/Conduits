using System.Collections.Generic;
using UnityEngine;

namespace Conduits.Core
{
    [CreateAssetMenu()]
    public class Level : ScriptableObject  
    {
        // TODO: Figure out how to serialize
        
        public Tile[,] Tiles;
        public int[] Inventory;
        // TODO: state
    }
}