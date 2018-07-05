using UnityEngine;

namespace Conduits.Core
{
    public class GameController : MonoBehaviour
    {
        public Level[] levels;
        Grid grid;

        void Awake()
        {
            var level = levels[0];
            grid = new Grid();
            grid.LoadLevel(level);
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