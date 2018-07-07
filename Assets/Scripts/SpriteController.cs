using Conduits.Core;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    GridController grid;

    public GridSprite GridSpritePrefab;
    public Sprite[] WireSprites;

    GridSprite[,] gridSprites;

    public void Initialize(GridController grid, int width, int height)
    {
        this.grid = grid;
        gridSprites = new GridSprite[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var gs = Instantiate(GridSpritePrefab);
                gridSprites[x, y] = gs;
                gs.gameObject.transform.position = new Vector2(x, y);
            }
        }
    }

    public void SpriteChanged (int x, int y, Tile t, bool isNeighbor = false)
    {
        if (t.type == TileType.Wire)
        {
            int neighborIndex = 0;
            if ((grid.GetProperty(x + 1, y) & TileProperty.ReadLeft) != 0) neighborIndex += 1;
            if ((grid.GetProperty(x, y + 1) & TileProperty.ReadDown) != 0) neighborIndex += 2;
            if ((grid.GetProperty(x - 1, y) & TileProperty.ReadRight) != 0) neighborIndex += 4;
            if ((grid.GetProperty(x, y - 1) & TileProperty.ReadUp) != 0) neighborIndex += 8;
            gridSprites[x, y].SetSprite(WireSprites[neighborIndex]);
        }
        if (!isNeighbor)
        {
            Tile neighbor;
            for (int i = 0; i < 4; i++)
            {
                int xOffset = Mathf.Abs(i - 2) - 1;
                int yOffset = -Mathf.Abs(i - 1) + 1;
                neighbor = grid.GetTile(x + xOffset, y + yOffset);
                if (neighbor != null) SpriteChanged(x + xOffset, y + yOffset, neighbor, true);
            }
        }
    }
}
