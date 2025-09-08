using UnityEngine;

namespace Levels.Info
{
    public class MapCell
    {
        private int x;
        private int y;

        private Vector3 position;

        public Vector2 ArrayPosition => new(x, y);
        public Vector3 WorldPosition => position;
    }
}