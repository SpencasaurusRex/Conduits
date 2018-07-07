using Conduits.Core;

namespace Conduits.Logic
{
    struct Wire : TileLogic
    {
        public TileState CalculateOutState(Tile self, int x, int y, TileSide side)
        {
            return self.state;
        }

        public TileState CalculateState(Tile self, int x, int y)
        {
            TileState searchState;
            switch (self.state)
            {
                case TileState.Off:
                    searchState = TileState.Start;
                    break;
                case TileState.On:
                    searchState = TileState.Stop;
                    break;
                case TileState.Start:
                    searchState = TileState.Stop;
                    return TileState.On; // TODO: Fix this to allow signals of length zero (start, stop).
                case TileState.Stop:
                    return TileState.Off; // TODO: Fix this to zero space between signals (stop, start).
                default:
                    throw new System.NotImplementedException("Unexpected state: " + self.state + " for tile at (" + x + "," + y + ")");
            }

            if (self.grid.GetNeighborState(x + 1, y, TileSide.Left) == searchState) return searchState;
            if (self.grid.GetNeighborState(x - 1, y, TileSide.Right) == searchState) return searchState;
            if (self.grid.GetNeighborState(x, y + 1, TileSide.Down) == searchState) return searchState;
            if (self.grid.GetNeighborState(x, y - 1, TileSide.Up) == searchState) return searchState;

            return self.state;
        }
    }
}