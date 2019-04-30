using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
    public class StartScreen : MonoBehaviour
    {
        public string FirstLevel;

        private void OnGUI()
        {
            GUILayout.BeginVertical();

            if(GUILayout.Button("Start Game"))
            {
                GameManagerInstance.Instance.StartGame();
                SceneManager.LoadScene(FirstLevel);
            }

            if (GUILayout.Button("High Scores"))
                UnityEngine.SceneManagement.SceneManager.LoadScene("HighScoreScreen");

            GUILayout.EndVertical();
        }
    }
}
