using UnityEngine;

namespace SparuvianConnection.Adoptatron.Utils
{
    public static class ScreenUtils
    {
        
        private static float _screenLeft;
        private static float _screenRight;
        private static float _screenTop;
        private static float _screenBottom;
        private static float _screenToWorldWidthFactor;
        private static float _screenToWorldHeightFactor;

        public static float WidthInPixels { get; private set; }
        public static float HeightInPixels { get; private set; }
        public static float WidthInWorldCoordinates { get; private set; }
        public static float HeightInWorldCoordinates { get; private set; }

        public static void Initialize()
        {
            WidthInPixels = Screen.width;
            HeightInPixels = Screen.height;
            
            Camera camera = Camera.main;

            if (camera != null)
            {
                float screenZ = -camera.transform.position.z;
                Vector3 lowerLeftCornerScreen = new Vector3(0, 0, screenZ);
                Vector3 upperRightCornerScreen = new Vector3(Screen.width, Screen.height, screenZ);
                Vector3 lowerLeftCornerWorld = camera.ScreenToWorldPoint(lowerLeftCornerScreen);
                Vector3 upperRightCornerWorld = camera.ScreenToWorldPoint(upperRightCornerScreen);
                
                _screenRight = upperRightCornerWorld.x;
                _screenLeft = lowerLeftCornerWorld.x;
                _screenTop = upperRightCornerWorld.y;
                _screenBottom = lowerLeftCornerWorld.y;

                WidthInWorldCoordinates = _screenRight - _screenLeft;
                HeightInWorldCoordinates = _screenTop - _screenBottom;
                
                _screenToWorldWidthFactor = WidthInWorldCoordinates / WidthInPixels;
                _screenToWorldHeightFactor = HeightInWorldCoordinates / HeightInPixels;
            }
        }

        public static Vector2 ScreenToWorldVector(Vector2 vector)
        {
            vector.x *= _screenToWorldWidthFactor;
            vector.y *= _screenToWorldHeightFactor;

            return vector;
        }
        
    }
}