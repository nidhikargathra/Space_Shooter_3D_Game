using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{

    private bool gotHit;
    private void Start()
    {
        gotHit = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        //if (!gotHit && col.transform.CompareTag("Player"))
        //{
        //    //load next level
        //    gotHit = true;
        //    Debug.Log("level 1 complete");
        //    SceneManager.LoadScene("Level2");
        //}
    }
}
