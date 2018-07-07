namespace Conduits.Core
{
    public interface TileLogic
    {
        /// <summary>
        /// Get's the new state of the tile given the read buffer.
        /// </summary>
        /// <param name="self">The tile during the previous tick</param>
        /// <param name="x">The x coordinate of the tile</param>
        /// <param name="y">The y coordinate of the tile</param>
        /// <param name="buffer">The tile array during the previous tick</param>
        /// <returns>What the new state of the tile should be</returns>
        TileState CalculateState(Tile self, int x, int y);

        /// <summary>
        /// Get's the state that a neighbor will retrieve from this tile
        /// </summary>
        /// <param name="self">The tile during the current tick</param>
        /// <param name="x">The x coordinate of the tile</param>
        /// <param name="y">The y coordinate of the tile</param>
        /// <param name="side"></param>
        /// <returns>How other tiles should read from this tile one tick in the future</returns>
        TileState CalculateOutState(Tile self, int x, int y, TileSide side);
    }
}
