using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
    public class StartScreen : MonoBehaviour
    {
        public string FirstLevel;
        public Texture Background;

        private float Width;
        private float Height;

        private void Awake()
        {
            Width = Screen.currentResolution.width;
            Height = Screen.currentResolution.height;
        }
        private void OnGUI()
        {
            var widthScaled = Screen.width / Width;
            var heightScaled = Screen.height / Height;

            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(widthScaled, heightScaled, 1));
            GUI.DrawTexture(new Rect(0, 0, Width, Height), Background);

            if (GUI.Button(new Rect(850 * Width/1920, 315 * Height/1080, 279 * Width / 1920, 100 * Height / 1080), ""))
            {
                GameManagerInstance.Instance.StartGame();
                SceneManager.LoadScene(FirstLevel);
            }

            if (GUI.Button(new Rect(850 * Width / 1920, 500 * Height / 1080, 279 * Width / 1920, 108 * Height / 1080), ""))
                SceneManager.LoadScene("HighScoreScreen");

            if (GUI.Button(new Rect(850 * Width / 1920, 700 * Height / 1080, 279 * Width / 1920, 100 * Height / 1080), ""))
                Application.Quit();
        }
    }
}
