using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour {
    [SerializeField] int numberOfAsteroidsOnAnAxis = 5;
    [SerializeField] int gridSpacing = 25;
    [SerializeField] Asteroid asteroidPrefab;

    List<Asteroid> asteroids = new List<Asteroid>();

    private void Start()
    {
        //PlaceAsteroids();
    }

    private void OnEnable()
    {
        EventManager.onStartGame += PlaceAsteroids;
        EventManager.onPlayerDeath += DestroyAsteroids;
    }

    private void OnDisable()
    {
        EventManager.onStartGame -= PlaceAsteroids;
        EventManager.onPlayerDeath -= DestroyAsteroids;
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
    }

    void DestroyAsteroids()
    {
        foreach (Asteroid ast in asteroids)
            ast.SelfDestruct();
        asteroids.Clear();
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


    float AsteroidOffset()
    {
        return Random.Range(-gridSpacing / 2f, gridSpacing / 2f);
    }
}
