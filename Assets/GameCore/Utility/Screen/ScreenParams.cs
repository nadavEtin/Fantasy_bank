using JetBrains.Annotations;
using UnityEngine;

namespace GameCore.Utility.Screen
{
    public class ScreenParams
    {
        public float ScreenWidth, ScreenHeight;
        public float RightEdgeXPos, LeftEdgeXPos;

        [NotNull] private Camera _cam;

        public ScreenParams()
        {
            _cam = Camera.main;
            ScreenHeight = _cam!.orthographicSize * 2f;
            ScreenWidth = ScreenHeight * _cam.aspect;
            RightEdgeXPos = ScreenWidth / 2;
            LeftEdgeXPos = ScreenWidth / -2;
        }
    }
}