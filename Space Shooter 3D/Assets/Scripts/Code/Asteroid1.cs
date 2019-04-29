using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Asteroid1 : MonoBehaviour {
        private const int MaxLevel = 6;
        private float 
            _alpha,
            _velocity;

        private Vector3 _direction;
        private AsteroidManager1 _asteroidManager;
        private Destroyable _destroyable;


        public int Level { get; private set; }
        public float DistanceSquared { get; private set; }
        public bool IsVisible { get; private set; }
        public bool IsActive { get; private set; }

        public void Awake()
        {
            _asteroidManager = FindObjectOfType<AsteroidManager1>();
            _destroyable = GetComponent<Destroyable>();
        }

        public void UpdatePlayerPosition(Vector3 playerPosition)
        {
            var point = Camera.main.WorldToViewportPoint(transform.position);
            IsVisible = point.x > -0.5f && point.x < 1.5f && point.y > -0.5f && point.y < 1.5f && point.z > 0;

            DistanceSquared = (transform.position - playerPosition).sqrMagnitude;
        }

        public void Update()
        {
            if (_alpha < 1)
            {
                _alpha = Mathf.Lerp(_alpha, 1, Time.deltaTime * 10);

                if (_alpha > 9.9f)
                    _alpha = 1;

                GetComponent<Renderer>().material.color = new Color(1, 1, 1, _alpha);
            }

            transform.Translate(_direction * _velocity * Time.deltaTime);
        }

        public void Init(Vector3 position, Vector3 rotation, Vector3 direction, Vector3 scale, float velocity)
        {
            Level = (int)Mathf.Ceil((scale.magnitude - 25f) / 255f * (MaxLevel - 1)) + 1;
            _destroyable.MaxHealth = _destroyable.Health = Level * 100;

            transform.position = position;
            transform.localEulerAngles = rotation;
            transform.localScale = scale;

            _velocity = velocity;
            _direction = direction;

            IsVisible = false;
        }

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);

            _alpha = 0;
            GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0);
        }

        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        public void Destroyed(GameObject from)
        {
            _asteroidManager.AsteroidDestroyed(this);
        }

        public void OnTriggerEnter(Collider other)
        {
            var shield = other.GetComponentInParent<Shield>();
            if (shield == null)
                return;
            shield.TakeDamage(Level * 50 / 3);
        }
    }
}
