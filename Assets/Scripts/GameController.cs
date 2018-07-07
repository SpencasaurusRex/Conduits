using UnityEngine;

namespace Conduits.Core
{
    public class GameController : MonoBehaviour
    {
        public Level[] levels;
        public SpriteController spriteControllerPrefab;

        GridController grid;

        void Awake()
        {
            var sc = Instantiate<SpriteController>(spriteControllerPrefab);
            var level = levels[0];
            grid = new GridController();
            grid.LoadLevel(level, sc);

        }

        void FixedUpdate()
        {
            grid.FixedUpdate();
        }

        void OnDrawGizmos()
        {
            if (grid != null) grid.OnDrawGizmos();
        }
    }
}