using UnityEngine;

namespace Conduits.Core
{
    public class Grid
    {
        Tile[,] primaryBuffer;
        Tile[,] secondaryBuffer;
        bool primary;

        public void LoadLevel(Level level)
        {
            // TODO: Load level
            //primaryBuffer = level.Tiles;
            primaryBuffer = new Tile[10, 10];
            secondaryBuffer = new Tile[10, 10];
            primaryBuffer[0, 0].state = TileState.On;
            primary = true;
        }

        public void OnDrawGizmos()
        {
            if (primaryBuffer == null || secondaryBuffer == null) return;

            var currentBuffer = primary ? primaryBuffer : secondaryBuffer;
            for (int y = 0; y < currentBuffer.GetLength(1); y++)
            {
                for (int x = 0; x < currentBuffer.GetLength(0); x++)
                {
                    Gizmos.color = currentBuffer[x, y].state == TileState.On ? Color.green : Color.black;
                    Gizmos.DrawCube(new Vector2(x, y), Vector2.one);
                }
            }
        }

        public void FixedUpdate()
        {
            var currentBuffer = primary ? primaryBuffer : secondaryBuffer;
            var nextBuffer = primary ? secondaryBuffer : primaryBuffer;
            for (int y = 0; y < currentBuffer.GetLength(1); y++)
            {
                for (int x = 0; x < currentBuffer.GetLength(0); x++)
                {
                    nextBuffer[x, y].state = AreNeighborsActive(x, y) ? TileState.On : TileState.Off;
                }
            }
            primary = !primary;
        }

        bool AreNeighborsActive(int x, int y)
        {
            // Diagonal
            //for (int yOffset = -1; yOffset <= 1; yOffset++)
            //{
            //    for (int xOffset = -1; xOffset <= 1; xOffset++)
            //    {
            //        if (yOffset == 0 && xOffset == 0) continue;
            //        int nx = x + xOffset;
            //        int ny = y + yOffset;

            //    }
            //}

            // Cardinal
            var buffer = primary ? primaryBuffer : secondaryBuffer;
            if (CheckNeighbor(x, y, buffer)) return true;
            for (int i = 0; i < 4; i++)
            {
                int xOffset = Mathf.Abs(i - 2) - 1;
                int yOffset = -Mathf.Abs(i - 1) + 1;
                if (CheckNeighbor(x + xOffset, y + yOffset, buffer))
                {
                    return true;
                }
            }
            return false;
        }

        bool CheckNeighbor(int nx, int ny, Tile[,] buffer)
        {
            if (0 <= nx && nx < buffer.GetLength(0) && 0 <= ny && ny < buffer.GetLength(1))
            {
                return buffer[nx, ny].state == TileState.On;
            }
            return false;
        }
    }
}