using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Code
{
    public class AsteroidManager1 : MonoBehaviour
    {
        private List<Asteroid1> _asteroids;
        private Player _player;
        private LevelManager _levelManager;

        public Asteroid1 AsteroidPrefab;

        public int MaxAsteroids;
        public int MaxVisibleAsteroids;

        private void Awake()
        {
            _asteroids = new List<Asteroid1>();
            _player = (Player)FindObjectOfType(typeof(Player));
            _levelManager = FindObjectOfType<LevelManager>();
        }
        private void Start()
        {
            for(int i = 0; i < MaxAsteroids; i++)
            {
                var asteroid = (Asteroid1)Instantiate(AsteroidPrefab);
                asteroid.gameObject.name = "Asteroid " + i;
                asteroid.Deactivate();
                _asteroids.Add(asteroid);
            }
        }
        private void LateUpdate()
        {
            var totalVisible = 0;
            if (_player == null)
                return;
            foreach(var asteroid in _asteroids.Where(a => a.IsActive))
            {
                asteroid.UpdatePlayerPosition(_player.transform.position);
                if (asteroid.IsVisible)
                    totalVisible++;
            }
            for(int i = 0; i < _asteroids.Count; i++)
            {
                var asteroid = _asteroids[i];
                if (asteroid.IsActive)
                {
                    if (asteroid.IsVisible)
                        continue;
                    if (asteroid.DistanceSquared < 50 * 50)
                        continue;
                }

                if (totalVisible < MaxVisibleAsteroids)
                {
                    ActivateAsteroid(asteroid);
                    totalVisible++;
                }
                else
                    asteroid.Deactivate();
            }
        }

        public void ActivateAsteroid(Asteroid1 asteroid)
        {
            asteroid.Activate();

            var rotation = new Vector3(
                UnityEngine.Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f));

            var direction = new Vector3(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f));

            var scale = Random.Range(0f, 1f) > 0.3f
                ? new Vector3(Random.Range(25f, 90f), Random.Range(25f, 90f), Random.Range(25f, 90f))
                : new Vector3(Random.Range(110f, 160f), Random.Range(110f, 160f), Random.Range(110f, 160f));

            var velocity = Random.Range(1f, 7f);

            var position = Camera.main.ViewportToWorldPoint(new Vector3(
                Random.Range(-.5f, 1.5f),
                Random.Range(-.5f, 1.5f),
                (Random.Range(0f, 1f) * 300) + 200));

            asteroid.Init(position, rotation, direction, scale, velocity);
        }

        public void AsteroidDestroyed(Asteroid1 asteroid1)
        {
            _levelManager.AsteroidDestroyedByPlayer(asteroid1);
            asteroid1.Deactivate();

            if (asteroid1.Level <= 3)
                return;

            var toCreate = (int)Mathf.Ceil(asteroid1.Level - 3) * Random.Range(1f, 2f);

            for(int i = 0; toCreate > 0 && i < _asteroids.Count; i++)
            {
                var inActiveAsteroid = _asteroids[i];
                if (inActiveAsteroid.IsActive)
                    continue;

                ActivateSubAsteroid(inActiveAsteroid, asteroid1.gameObject.transform.position);
                toCreate--;

            }
        }

        private void ActivateSubAsteroid(Asteroid1 asteroid, Vector3 spawnNextTo)
        {
            asteroid.Activate();

            var rotation = new Vector3(
                UnityEngine.Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f));

            var direction = new Vector3(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f));

            var scale = new Vector3(Random.Range(15f, 40f), Random.Range(15f, 40f), Random.Range(15f, 40f));
               
            var velocity = Random.Range(20f, 25f);

            var position = spawnNextTo + direction * 5;

            asteroid.Init(position, rotation, direction, scale, velocity);
        }
    }

}
