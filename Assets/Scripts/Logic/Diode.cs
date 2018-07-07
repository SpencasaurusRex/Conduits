using Conduits.Core;
using UnityEngine;

namespace Conduits.Logic
{
    struct Diode : TileLogic
    {
        public TileSide outputSide;

        Vector2Int inputOffset;
        public Diode(TileSide outputSide)
        {
            this.outputSide = outputSide;
            inputOffset = Tile.SideToOffset(outputSide).Negate();
        }

        public TileState CalculateOutState(Tile self, int x, int y, TileSide side)
        {
            if (side == outputSide) return self.state;
            return TileState.Off;
        }

        public TileState CalculateState(Tile self, int x, int y)
        {
            int nx = x + inputOffset.x;
            int ny = y + inputOffset.y;
            return self.grid.GetNeighborState(nx, ny, outputSide);
        }
    }
}