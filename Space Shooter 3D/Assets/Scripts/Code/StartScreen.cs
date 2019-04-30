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

        private const float Width = 1920;
        private const float Height = 1080;

        private void OnGUI()
        {
            var widthScaled = Screen.width / Width;
            var heightScaled = Screen.height / Height;

            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(widthScaled, heightScaled, 1));
            GUI.DrawTexture(new Rect(0, 0, Width, Height), Background);

            if (GUI.Button(new Rect(850, 315, 279, 100), ""))
            {
                GameManagerInstance.Instance.StartGame();
                SceneManager.LoadScene(FirstLevel);
            }

            if (GUI.Button(new Rect(850, 500, 279, 108), ""))
                SceneManager.LoadScene("HighScoreScreen");

            if (GUI.Button(new Rect(850, 700, 279, 100), ""))
                Application.Quit();
        }
    }
}
