using UnityEngine;

namespace Game.Untils.ScreenBounds
{
    public static class ScreenBounds
    {
        private static Vector3 _bottomLeft;
        private static Vector3 _topRight;

        public static Vector3 TopRight => _topRight;
        public static Vector3 BottomLeft => _bottomLeft;


        static ScreenBounds()
        {
            var camera = Camera.main;
            _topRight = camera.ScreenToWorldPoint(new Vector3(camera.pixelWidth, camera.pixelHeight, camera.depth));
            _bottomLeft = camera.ScreenToWorldPoint(new Vector3(0, 0, camera.depth));
        }
    }
}
