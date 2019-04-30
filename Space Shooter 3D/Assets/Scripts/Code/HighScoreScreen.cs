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

        private const float Width = 1920;
        private const float Height = 1080;

        public void OnGUI()
        {
            var widthScaled = Screen.width / Width;
            var heightScaled = Screen.height / Height;

            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(widthScaled, heightScaled, 1));
            GUI.DrawTexture(new Rect(0, 0, Width, Height), Background);

            var scores = HighScoreManager.Instance.Scores;

            GUILayout.BeginArea(new Rect(173, 186, 1035, 724));

            foreach (var score in scores)
            {
                GUILayout.BeginHorizontal(RowStyle);

                GUILayout.Label(score.Name, TextStyle);
                GUILayout.Label(string.Format("{0} points", score.Points), PointStyle);

                GUILayout.EndHorizontal();
            }

            GUILayout.EndArea();

            if (GUI.Button(new Rect(845, 960, 205, 71), ""))
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
        }
    }
}