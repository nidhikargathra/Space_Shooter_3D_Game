using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class GameManagerInstance : MonoBehaviour
    {
        public static GameManager Instance { get; private set;} 
        public bool IsDebug;

        private void Awake()
        {
            if(Instance == null)
            { var manager = new GameObject();
                Instance = manager.AddComponent<GameManager>();
                Instance.IsDebug = IsDebug;
            }
            Destroy(gameObject);
        }
    }
}
