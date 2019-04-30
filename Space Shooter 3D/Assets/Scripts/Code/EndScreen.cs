﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
    public class EndScreen : MonoBehaviour
    {
        public Texture WinBackground, LoseBackground;
        public GUIStyle TextStyle;

        private string _name = "";
        private bool _hasAddedScore;

        private const float Width = 1920;
        private const float Height = 1080;

        private GUIStyle _buttonStyle, _textFieldStyle;

        private void OnGUI()
        {
            if(_buttonStyle == null)
            {
                _buttonStyle = new GUIStyle(GUI.skin.button)
                {
                    fontSize = 32
                };
                _textFieldStyle = new GUIStyle(GUI.skin.textField)
                {
                    fontSize = 32,
                    margin =
                    {
                        bottom = 20
                    }
                };
            }
            var widthScaled = Screen.width / Width;
            var heightScaled = Screen.height / Height;

            GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(widthScaled, heightScaled, 1));
            
            if(GameManagerInstance.Instance.DidWin)
                GUI.DrawTexture(new Rect(0, 0, Width, Height), WinBackground);
            else
                GUI.DrawTexture(new Rect(0, 0, Width, Height), LoseBackground);

            GUILayout.BeginArea(new Rect(496, 272, 600, 538));

            GUILayout.BeginVertical();

            GUILayout.Label(string.Format(" With {0} points", GameManagerInstance.Instance.Points), TextStyle);
            
            ///// TO change behavior in case player lost.. remove these lines
            if (HighScoreManager.Instance.CanAddHighScore(GameManagerInstance.Instance.Points))
            {
                if (!_hasAddedScore)
                {
                    GUILayout.Label("You got a high score! Enter your name: ", TextStyle);
                    _name = GUILayout.TextField(_name, _textFieldStyle);

                    if (GUILayout.Button("Save Score", _buttonStyle))
                    {
                        HighScoreManager.Instance.AddHighScore(_name, GameManagerInstance.Instance.Points);
                        _hasAddedScore = true;
                    }
                }
                else
                    GUILayout.Label("Score Added", TextStyle);
            }
            else
            {
                GUILayout.Label("You didn't get a high score :(", TextStyle);
            }
            /////
            

            GUILayout.EndVertical();

            GUILayout.EndArea();

            if(!GameManagerInstance.Instance.DidWin && GUI.Button(new Rect(843, 840, 240, 86), ""))
                SceneManager.LoadScene("StartScreen");

            if (GameManagerInstance.Instance.DidWin && GUI.Button(new Rect(822, 895, 240, 86), ""))
                SceneManager.LoadScene("StartScreen");
        }
    }
}