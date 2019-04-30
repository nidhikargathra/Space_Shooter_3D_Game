using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class EndScreen : MonoBehaviour
    {
        private string _name = "";
        private bool _hasAddedScore;

        private void OnGUI()
        {
            GUILayout.BeginVertical();

            if (GameManagerInstance.Instance.DidWin)
            {
                GUILayout.Label("You won");
            }
            else
                GUILayout.Label("You lost");

            GUILayout.Label(string.Format(" With {0} points", GameManagerInstance.Instance.Points));
            ///// TO change behavior in case player lost.. remove these lines
            if (HighScoreManager.Instance.CanAddHighScore(GameManagerInstance.Instance.Points))
            {
                if (!_hasAddedScore)
                {
                    GUILayout.Label("You got a high score! Enter your name: ");
                    _name = GUILayout.TextField(_name);

                    if (GUILayout.Button("Save Score"))
                    {
                        HighScoreManager.Instance.AddHighScore(_name, GameManagerInstance.Instance.Points);
                        _hasAddedScore = true;
                    }
                }
                else
                    GUILayout.Label("Score Added");
            }
            else
            {
                GUILayout.Label("You didn't get a high score :(");
            }
            /////
            if (GUILayout.Button("Close"))
                UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");

            GUILayout.EndVertical();
        }
    }
}