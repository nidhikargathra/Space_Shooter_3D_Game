﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
    public class LevelManager : MonoBehaviour
    {
        public float TimeLeft;
        public string NextLevel;

        private void Update()
        {
            TimeLeft -= Time.deltaTime;

            if (TimeLeft < 0)
                GameManagerInstance.Instance.EndGame(false);
        }

        public void AsteroidDestroyedByPlayer(Asteroid1 asteroid)
        {
            GameManagerInstance.Instance.Points += asteroid.Level * 50;
        }

        public void WaypointsHitByPlayer(Waypoint waypoint)
        {
            if(waypoint.next == null)
            {
                if (!string.IsNullOrEmpty(NextLevel))
                {
                    if (GameManagerInstance.Instance.IsDebug)
                        Debug.Log("You are moving on to the next level!");
                    else
                        SceneManager.LoadScene(NextLevel);
                }
                else
                    GameManagerInstance.Instance.EndGame(true);
            }

            TimeLeft += waypoint.TimeModifier;
        }
    }
}
