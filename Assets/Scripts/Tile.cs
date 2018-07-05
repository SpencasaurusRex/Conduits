using UnityEngine;
using UnityEditor;

namespace Conduits.Core
{
    [System.Serializable]
    public enum TileState 
    {
        Off,
        On
    }

    [System.Serializable]
    public enum TileType
    {
        None,
        Switch,
        Light,
        Wire
    }

    [System.Serializable]
    public struct Tile 
    {
        public TileState state;
        public TileType type;
    }
}