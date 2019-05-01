using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerGUI
    {
        private float Width;
        private float Height;
        public float CurrentCursorSize = 20f;
       
        public static Texture2D image = (Texture2D) Resources.Load("crosshair");
        public static Texture2D OffscreenIndicatorImage = (Texture2D)Resources.Load("OffscreenIndicator");
        public static Texture2D TargetSquareImage = (Texture2D)Resources.Load("TargetSquare");

       

        private PlayerController _controller;

        public PlayerGUI(PlayerController controller, Player player)
        {
            _controller = controller;
        }

        private void Awake()
        {
            Width = Screen.currentResolution.width;
            Height = Screen.currentResolution.height;
            CurrentCursorSize = 20f * Width / 1920f;
        }
        public void OnGUI()
        {
            //GUI.Label(new Rect(10 * Width / 1920, 10 * Height / 1080, 200 * Width / 1920, 40 * Height / 1080), string.Format("{0} points", GameManagerInstance.Instance.Points));
            //GUI.Label(new Rect(10, 10, 400, 80), string.Format("{0} points", GameManagerInstance.Instance.Points));
            GUIStyle TextStyle = new GUIStyle();
            TextStyle.fontSize = 32;
            TextStyle.normal.textColor = Color.white;
            GUILayout.BeginArea(new Rect(10, 10, 400, 80));
            GUILayout.Label(string.Format("{0} points", GameManagerInstance.Instance.Points), TextStyle);
            GUILayout.EndArea();
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
