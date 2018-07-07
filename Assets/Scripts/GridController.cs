using UnityEngine;

namespace Conduits.Core
{
    public class GridController
    {
        SpriteController spriteController;

        Tile[,] primaryBuffer;
        Tile[,] secondaryBuffer;

        int width;
        int height;

        Tile[,] WriteBuffer
        {
            get
            {
                return primary ? secondaryBuffer : primaryBuffer;
            }
        }

        public Tile[,] ReadBuffer
        {
            get
            {
                return primary ? primaryBuffer : secondaryBuffer;
            }
        }

        bool primary;

        #region Levels
        void Rectangles(int size)
        {
            Initialize(size, size, Tile.None);
            Square(size);
            for (int i = 0; i < width; i++)
            {
                SetBoth(i, height / 2, Tile.WireOff);
            }
            SetBoth(0, 0, Tile.WireStop);
            SetBoth(1, 0, Tile.WireOn);
            SetBoth(2, 0, Tile.WireOn);
            SetBoth(3, 0, Tile.WireStart);
        }

        void Squares()
        {
            Initialize(20, 20, Tile.None);

            for (int i = 0; i < width; i++)
            {
                SetBoth(i, 0, Tile.WireOff);
            }

            for (int l = 3; l <= 15; l += 2)
            {
                for (int i = 0; i < l; i++)
                {
                    SetBoth(width - l, i, Tile.WireOff);
                    SetBoth(width - 1, i, Tile.WireOff);
                    SetBoth(width - l + i, l - 1, Tile.WireOff);
                }
            }

            for (int i = 0; i < 3; i++)
            {
                SetBoth(3, i, Tile.WireOff);
                SetBoth(5, i, Tile.WireOff);
                SetBoth(5 - i, 2, Tile.WireOff);
            }

            SetBoth(0, 0, Tile.WireStop);
            SetBoth(1, 0, Tile.WireOn);
            SetBoth(2, 0, Tile.WireOn);
            SetBoth(3, 0, Tile.WireOn);
            SetBoth(4, 0, Tile.WireOn);
            SetBoth(5, 0, Tile.WireStart);
        }

        void Square(int size)
        {
            Initialize(size, size, Tile.None);
            for (int i = 0; i < size; i++)
            {
                SetBoth(i, 0, Tile.WireOff);
                SetBoth(width - 1, i, Tile.WireOff);
                SetBoth(i, height - 1, Tile.WireOff);
                SetBoth(0, i, Tile.WireOff);
            }
            SetBoth(0, 0, Tile.WireStop);
            SetBoth(1, 0, Tile.WireOn);
            SetBoth(2, 0, Tile.WireStart);
        }


        void BaseSquare(int size, int left = 0, int bottom = 0)
        {
            for (int i = 0; i < size; i++)
            {
                SetBoth(i + left, bottom, Tile.WireOff);
                SetBoth(size - 1 + left, i + bottom, Tile.WireOff);
                SetBoth(i + left, size - 1 + bottom, Tile.WireOff);
                SetBoth(0 + left, i + bottom, Tile.WireOff);
            }
        }

        void BaseRectangle(int width, int height, int left = 0, int bottom = 0)
        {
            for (int i = 0; i < width; i++)
            {
                SetBoth(left + i, bottom, Tile.WireOff);
                SetBoth(left + i, bottom + height - 1, Tile.WireOff);
            }
            for (int i = 0; i < height; i++)
            {
                SetBoth(left, bottom + i, Tile.WireOff);
                SetBoth(left + width - 1, bottom + i, Tile.WireOff);
            }
        }

        void NonLoopClock()
        {
            Initialize(3, 5, Tile.None);
            SetBoth(0, 0, Tile.WireStop);
            SetBoth(1, 0, Tile.WireOn);
            SetBoth(2, 0, Tile.WireOn);
            SetBoth(0, 1, Tile.WireOff);
            SetBoth(1, 1, Tile.WireOff);
            SetBoth(2, 1, Tile.WireStart);
            SetBoth(2, 2, Tile.WireOff);
            SetBoth(2, 3, Tile.WireOff);
            SetBoth(2, 4, Tile.WireOff);

        }

        void DiodeTest(int size)
        {
            Initialize(size, size, Tile.None);
            BaseSquare(size);

            // Remove bottom row
            for (int i = 0; i < width; i++)
            {
                SetBoth(i, 0, Tile.None);
            }

            // Large Clock
            BaseRectangle(5, 5);
            // Small Clock
            BaseRectangle(4, 4, width - 4);

            // Diodes
            SetBoth(0, height / 2 + 1, Tile.DiodeUp);
            SetBoth(width - 1, height / 2 + 1, Tile.DiodeDown);

            // Start clocks
            SetBoth(0, 0, Tile.WireStart);
            SetBoth(1, 0, Tile.WireOn);
            SetBoth(2, 0, Tile.WireStop);

            SetBoth(width - 1, 0, Tile.WireStart);
            SetBoth(width - 2, 0, Tile.WireOn);
            SetBoth(width - 3, 0, Tile.WireStop);
        }

        void Initialize(int width, int height, Tile defaultTile)
        {
            this.width = width;
            this.height = height;
            primaryBuffer = new Tile[width, height];
            secondaryBuffer = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    SetBoth(x, y, defaultTile);
                }
            }
        }
        #endregion Levels

        public void LoadLevel(Level level, SpriteController sr)
        {
            spriteController = sr;
            sr.Initialize(this, 10, 10);

            // TODO: Load level
            primaryBuffer = secondaryBuffer = level.Tiles;

            //Squares();
            //Square(20);
            //DiodeTest(10);
            //Rectangles(10);
            NonLoopClock();

            primary = true;
            
        }

        void SetBoth(int x, int y, Tile t)
        {
            var newTile = primaryBuffer[x, y] = t.Clone(this);
            secondaryBuffer[x, y] = t.Clone(this);
            spriteController.SpriteChanged(x, y, newTile);
        }

        public void OnDrawGizmos()
        {
            if (ReadBuffer == null) return;
            for (int y = ReadBuffer.GetLength(1) - 1; y >= 0; y--)
            {
                for (int x = 0; x < ReadBuffer.GetLength(0); x++)
                {
                    Tile t = ReadBuffer[x, y];
                    if (t.type == TileType.Wire)
                    {
                        if (t.state == TileState.Off)
                        {
                            Gizmos.color = Color.gray;
                        }
                        else if (t.state == TileState.On)
                        {
                            Gizmos.color = Color.green;
                        }
                        else if (t.state == TileState.Start)
                        {
                            Gizmos.color = Color.yellow;
                        }
                        else
                        {
                            Gizmos.color = Color.blue;
                        }
                    }
                    else if (t.type == TileType.Diode)
                    {
                        Gizmos.color = Color.magenta;
                    }
                    else
                    {
                        continue;
                    }
                    Gizmos.DrawCube(new Vector2(x, y), Vector3.one);
                }
            }
        }

        public void FixedUpdate()
        {
            for (int y = 0; y < ReadBuffer.GetLength(1); y++)
            {
                for (int x = 0; x < ReadBuffer.GetLength(0); x++)
                {
                    Tile t = ReadBuffer[x, y];
                    if (t.logic != null)
                    {
                        WriteBuffer[x, y].state = t.CalculateState(x, y);
                    }
                }
            }
            primary = !primary;
        }

        public bool BoundsCheck(int nx, int ny)
        {
            return 0 <= nx && nx < width && 0 <= ny && ny < height;
        }

        public Tile GetTile(int x, int y)
        {
            if (BoundsCheck(x, y))
            {
                return ReadBuffer[x, y];
            }
            else
            {
                return null;
            }
        }

        public TileProperty GetProperty(int x, int y)
        {
            var t = GetTile(x, y);
            if (t == null) return TileProperty.None;
            return t.property;
        }

        public TileState GetNeighborState(int nx, int ny, TileSide neighborsSide, TileState defaultState = TileState.Off)
        {
            var t = GetTile(nx, ny);
            if (t == null || ((t.property & TileProperty.Sender) == 0)) return defaultState;
            return t.CalculateOutState(nx, ny, neighborsSide);
        }
    }
}
