using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class AutoDestroyParticleSystem : MonoBehaviour
    {
        public ParticleSystem System;
        private void Update()
        {
            if (!System.IsAlive(true))
                Destroy(gameObject);
        }
    }
}
