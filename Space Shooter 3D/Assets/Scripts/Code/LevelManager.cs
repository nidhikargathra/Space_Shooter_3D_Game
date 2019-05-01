using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
    public class LevelManager : MonoBehaviour
    {
        public float TimeLeft;
        public string NextLevel;

        private float maxTime;
        private TimerUI _timerUI;
        private void Start()
        {
            _timerUI = FindObjectOfType<TimerUI>();
            maxTime = TimeLeft;
        }
        private void Update()
        {
            TimeLeft -= Time.deltaTime;

            if (TimeLeft < 0)
                GameManagerInstance.Instance.EndGame(false);

            _timerUI.UpdateTimerDisplay(TimeLeft, maxTime);
        }

        public void AsteroidDestroyedByPlayer(Asteroid1 asteroid)
        {
            GameManagerInstance.Instance.Points += asteroid.Level * 50;
        }

        public void WaypointsHitByPlayer(Waypoint waypoint)
        {
            if (waypoint.next == null)
            {
                GameManagerInstance.Instance.Points += (int)Mathf.Ceil(TimeLeft) * 10;
                if (!string.IsNullOrEmpty(NextLevel))
                {
                    if (GameManagerInstance.Instance.IsDebug)
                        Debug.Log("You are moving on to the next level!");
                    else
                    {
                        StartCoroutine("LoadSceneAfterWait");
                        
                    }
                }
                else
                    GameManagerInstance.Instance.EndGame(true);
            }

            TimeLeft += waypoint.TimeModifier;
            if (maxTime < TimeLeft)
                maxTime = TimeLeft;
            
        }

        public void EnemyDestroyedByPlayer()
        {
            GameManagerInstance.Instance.Points += 500;
        }

        public void PlayerDied()
        {
            GameManagerInstance.Instance.EndGame(false);
        }

        public IEnumerator LoadSceneAfterWait()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(NextLevel);
        }
    }

}