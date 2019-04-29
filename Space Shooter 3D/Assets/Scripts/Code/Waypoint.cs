using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Waypoint : MonoBehaviour
    {
        public Waypoint next;
        public float 
            TimeModifier, 
            MinimumVelocity;
        public Shader AlphaShader;

        private WaypointManager _manager;
        private IEnumerable<Renderer> _renderers;

        private bool _deactivating;
        private float _alpha;

        private void Start()
        {
            _manager = FindObjectOfType<WaypointManager>();
            _alpha = 1;
            _renderers = GetComponentsInChildren<Renderer>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponentInParent<Player>() == null)
                return;
            _manager.PlayerHitWaypoint(this);
        }
        public void Deactivate()
        {
            _deactivating = true;
            foreach (var render in _renderers)
                render.material.shader = AlphaShader;
        }

        private void Update()
        {
            if (!_deactivating)
                return;

            _alpha = Mathf.Lerp(_alpha, 0, Time.deltaTime * 5);

            foreach(var render in _renderers)
                render.material.color = new Color(1, 1, 1, _alpha);

            if (_alpha < 0.01f)
                gameObject.SetActive(false);
        }
    }
}