using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnTimer = 5f;
    [SerializeField] float TimeToStopSpawning = 30f;

    private float timePassed;
    private bool keepTime;
    void Start()
    {
        keepTime = true;
        timePassed = 0;
        StartSpawning();    
    }
    private void Update()
    {
        if (keepTime)
        {
            timePassed += Time.deltaTime;
            if (timePassed >= TimeToStopSpawning)
            {
                StopSpawning();
                keepTime = false;
            }
        }
    }
    //private void OnEnable()
    //{
    //    EventManager.onStartGame += StartSpawning;
    //    EventManager.onPlayerDeath += StopSpawning;
    //}

    //private void OnDisable()
    //{
    //    EventManager.onStartGame -= StartSpawning;
    //    EventManager.onPlayerDeath -= StopSpawning;
    //}
    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }

    void StartSpawning()
    {
        InvokeRepeating("SpawnEnemy", spawnTimer, spawnTimer);
    }
    void StopSpawning()
    {
        CancelInvoke();
    }
}
