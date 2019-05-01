using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class HighScoreScreen : MonoBehaviour
    {
        public Texture Background;
        public GUIStyle
            RowStyle,
            TextStyle,
            PointStyle;

        private float Width;
        private float Height;

        private void Awake()
        {
            Width = Screen.currentResolution.width;
            Height = Screen.currentResolution.height;
        }

        public void OnGUI()
        {
            var widthScaled = Screen.width / Width;
            var heightScaled = Screen.height / Height;

            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(widthScaled, heightScaled, 1));
            GUI.DrawTexture(new Rect(0, 0, Width, Height), Background);

            var scores = HighScoreManager.Instance.Scores;

            GUILayout.BeginArea(new Rect(173 * Width / 1920, 186 * Height / 1080, 1035 * Width / 1920, 724 * Height / 1080));
            //GUILayout.BeginArea(new Rect(173, 186, 1035, 724));

            foreach (var score in scores)
            {
                GUILayout.BeginHorizontal(RowStyle);

                GUILayout.Label(score.Name, TextStyle);
                GUILayout.Label(string.Format("{0} points", score.Points), PointStyle);

                GUILayout.EndHorizontal();
            }

            GUILayout.EndArea();

            if (GUI.Button(new Rect(845 * Width / 1920, 960 * Height / 1080, 205 * Width / 1920, 71 * Height / 1080), ""))
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
        }
    }
}