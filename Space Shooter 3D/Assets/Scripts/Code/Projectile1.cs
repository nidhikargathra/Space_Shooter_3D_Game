using UnityEngine;
using System.Collections;

namespace Assets.Code
{
    public class Projectile1 : MonoBehaviour
    {
        private float _timeToLive;
        private Vector3 _direction;
        private ProjectileWeapon _weapon;

        public ParticleSystem Effect;

        public void Init(ProjectileWeapon weapon, Vector3 direction)
        {
            transform.LookAt(direction + transform.position);

            _weapon = weapon;
            _timeToLive = weapon.TimeToLive;
            _direction = direction;
        }

        public void Update()
        {
            if((_timeToLive -= Time.deltaTime) < 0)
            {
                Destroy(gameObject);
                return;
            }

            transform.Translate(_direction * _weapon.Speed * Time.deltaTime, Space.World);
        }

        public void OnTriggerEnter(Collider other)
        {
            Debug.Log("projectile on trigger by "+other.name);
            var destroyable = other.GetComponentInParent<Destroyable>();
            if (destroyable == null)
                return;
            Debug.Log("weapon dam: "+ (int)_weapon.Damage);
            destroyable.TakeDamage((int)_weapon.Damage, gameObject);
            Instantiate(Effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("projectile on collision by " + collision.gameObject.name);
            var destroyable = collision.gameObject.GetComponent<Destroyable>();
            if (destroyable == null)
                return;
            destroyable.TakeDamage((int)_weapon.Damage, gameObject);
            Instantiate(Effect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
