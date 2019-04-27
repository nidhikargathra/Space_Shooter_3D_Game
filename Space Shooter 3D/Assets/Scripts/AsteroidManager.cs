using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    [SerializeField] int numberOfAsteroidsOnAnAxis = 5;
    [SerializeField] int gridSpacing = 25;
    [SerializeField] Asteroid asteroidPrefab;
    [SerializeField] GameObject pickupBatteryPrefab;
    [SerializeField] GameObject pickupOrbPrefab;

    List<Asteroid> asteroids = new List<Asteroid>();
    List<GameObject> pickups = new List<GameObject>();

    private void Start()
    {
        //PlaceAsteroids();
    }

    private void OnEnable()
    {
        EventManager.onStartGame += PlaceAsteroids;
        EventManager.onPlayerDeath += DestroyAsteroids;
        EventManager.onPlayerDeath += DestroyPickups;
        EventManager.onRespawnPickup += PlacePickup;
    }

    private void OnDisable()
    {
        EventManager.onStartGame -= PlaceAsteroids;
        EventManager.onPlayerDeath -= DestroyAsteroids;
        EventManager.onPlayerDeath -= DestroyPickups;
        EventManager.onRespawnPickup -= PlacePickup;
    }


    void PlaceAsteroids()
    {
        for(int x = 0; x < numberOfAsteroidsOnAnAxis; x++)
        {
            for (int y = 0; y < numberOfAsteroidsOnAnAxis; y++)
            {
                for (int z = 0; z < numberOfAsteroidsOnAnAxis; z++)
                {
                    InstantiateAsteroid(x, y, z);
                }
            }
        }
        PlacePickup();
    }

    void DestroyAsteroids()
    {
        foreach (Asteroid ast in asteroids)
            ast.SelfDestruct();
        asteroids.Clear();
    }

    void DestroyPickups()
    {
        foreach (GameObject obj in pickups)
            Destroy(obj);
        pickups.Clear();
    }

    void InstantiateAsteroid(int x, int y, int z)
    {
        Asteroid temp = Instantiate(asteroidPrefab, 
                    new Vector3(transform.position.x + (x * gridSpacing) + AsteroidOffset(), 
                                transform.position.y + (y * gridSpacing) + AsteroidOffset(), 
                                transform.position.z + (z * gridSpacing) + AsteroidOffset()), 
                    Quaternion.identity, 
                    transform) as Asteroid;
        asteroids.Add(temp);
    }

    void PlacePickup()
    {
        int rnd = Random.Range(0, asteroids.Count);
        GameObject temp = Instantiate(pickupBatteryPrefab, asteroids[rnd].transform.position, Quaternion.identity);
        pickups.Add(temp);
        Destroy(asteroids[rnd].gameObject);
        asteroids.RemoveAt(rnd);

        rnd = Random.Range(0, asteroids.Count);
        temp = Instantiate(pickupOrbPrefab, asteroids[rnd].transform.position, Quaternion.identity);
        pickups.Add(temp);
        Destroy(asteroids[rnd].gameObject);
        asteroids.RemoveAt(rnd);

        rnd = Random.Range(0, asteroids.Count);
        temp = Instantiate(pickupOrbPrefab, asteroids[rnd].transform.position, Quaternion.identity);
        pickups.Add(temp);
        Destroy(asteroids[rnd].gameObject);
        asteroids.RemoveAt(rnd);

        rnd = Random.Range(0, asteroids.Count);
        temp = Instantiate(pickupOrbPrefab, asteroids[rnd].transform.position, Quaternion.identity);
        pickups.Add(temp);
        Destroy(asteroids[rnd].gameObject);
        asteroids.RemoveAt(rnd);
    }

    float AsteroidOffset()
    {
        return Random.Range(-gridSpacing / 2f, gridSpacing / 2f);
    }
}
