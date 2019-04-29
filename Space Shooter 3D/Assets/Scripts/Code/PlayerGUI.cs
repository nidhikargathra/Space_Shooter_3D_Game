using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerGUI
    {
        public float CurrentCursorSize = 20f;
        public static Texture2D image = (Texture2D) Resources.Load("crosshair");
        public static Texture2D OffscreenIndicatorImage = (Texture2D)Resources.Load("OffscreenIndicator");
        public static Texture2D TargetSquareImage = (Texture2D)Resources.Load("TargetSquare");

        private PlayerController _controller;

        public PlayerGUI(PlayerController controller, Player player)
        {
            _controller = controller;
        }
            

        public void OnGUI()
        {
            GUI.DrawTexture(
                new Rect(
                    _controller.MousePosition.x - (CurrentCursorSize / 2),
                    Screen.height - _controller.MousePosition.y - (CurrentCursorSize / 2),
                    CurrentCursorSize,
                    CurrentCursorSize
                    ),
                image
                );
        }
    }

}
