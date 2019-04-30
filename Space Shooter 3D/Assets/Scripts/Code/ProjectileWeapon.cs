using UnityEngine;
using System.Collections;

namespace Assets.Code
{
    public class ProjectileWeapon : BasicWeapon
    {
        public Projectile1 prefab;
        public AudioClip Sound;
        public float
            Speed,
            Damage,
            FireRate,
            TimeToLive;

        private float _coolDown;

        public override void Fire(Vector3 position, Vector3 direction)
        {
            if ((_coolDown -= Time.deltaTime) > 0)
                return;

            AudioSource.PlayClipAtPoint(Sound, position, 0.4f);

            var projectile = (Projectile1)Instantiate(prefab, position, Quaternion.identity);
            projectile.Init(this, direction);

            _coolDown = FireRate;
        }
    }
}
