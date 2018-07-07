using Conduits.Logic;
using System;
using UnityEngine;

namespace Conduits.Core
{
    public enum TileSide
    {
        Right,
        Up,
        Left,
        Down
    }

    public enum TileState
    {
        Off,
        On,
        Start,
        Stop
    }

    public enum TileType
    {
        None,
        Switch,
        Light,
        Wire,
        Diode
    }

    public enum TileProperty
    {
        None = 0,

        ReadRight = 1, // Reads the state of it's neighbors
        ReadUp = 2,
        ReadLeft = 4,
        ReadDown = 8,
        ReadAny = 15,

        Sender = 16, // Allows neighbors to read it's state
        All = 31,
    }

    [System.Serializable]
    public class Tile
    {
        public TileState state;
        public TileType type;
        public TileProperty property;
        public TileLogic logic;
        public GridController grid;

        public Tile(TileState state, TileType type, TileProperty property, TileLogic logic, GridController grid)
        {
            this.state = state;
            this.type = type;
            this.property = property;
            this.logic = logic;
            this.grid = grid;
        }

        public Tile Clone(GridController grid)
        {
            return new Tile(state, type, property, logic, grid);
        }

        #region Calculate Methods
        public TileState CalculateState(int x, int y)
        {
            return logic.CalculateState(this, x, y);
        }

        public TileState CalculateOutState(int x, int y, TileSide side)
        {
            return logic.CalculateOutState(this, x, y, side);
        }
        #endregion Calculate Methods

        #region Utility

        public static Vector2Int SideToOffset(TileSide side)
        {
            int x = Math.Abs((int)side - 2) - 1;
            int y = -Math.Abs((int)side - 1) + 1;
            return new Vector2Int(x, y);
        }
        #endregion Utility

        #region Static Definitions
        public static Tile WireOff = new Tile(TileState.Off, TileType.Wire, TileProperty.All, new Wire(), null);
        public static Tile WireOn = new Tile(TileState.On, TileType.Wire, TileProperty.All, new Wire(), null);
        public static Tile WireStart = new Tile(TileState.Start, TileType.Wire, TileProperty.All, new Wire(), null);
        public static Tile WireStop = new Tile(TileState.Stop, TileType.Wire, TileProperty.All, new Wire(), null);
        public static Tile None = new Tile(TileState.Off, TileType.None, TileProperty.None, null, null);
        public static Tile DiodeUp = new Tile(TileState.Off, TileType.Diode, TileProperty.All, new Diode(TileSide.Up), null);
        public static Tile DiodeDown = new Tile(TileState.Off, TileType.Diode, TileProperty.All, new Diode(TileSide.Down), null);
        public static Tile DiodeRight = new Tile(TileState.Off, TileType.Diode, TileProperty.All, new Diode(TileSide.Right), null);
        #endregion Static Definitions
    }
}
