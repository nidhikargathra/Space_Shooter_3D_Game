using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Code
{
    public class GameManager : MonoBehaviour
    {
        public bool IsDebug { get;  set; }
        public int Points;
        public bool DidWin { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void StartGame()
        {
            Points = 0;
            DidWin = false;
        }

        public void EndGame(bool didWin)
        {
            if (IsDebug)
            {
                //Debug.Log("The game ended! ");
                return;
            }

            DidWin = didWin;
            SceneManager.LoadScene("EndScreen");
        }
        public IEnumerator LoadSceneAfterWait()
        {
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene("EndScreen");
        }
    }
}