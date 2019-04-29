using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class WaypointManager : MonoBehaviour
    {
        public Waypoint FirstWayPoint;

        private Waypoint _currentWaypoint;
        private Player _player;
        private LevelManager _levelManager;

        private void Start()
        {
            _currentWaypoint = FirstWayPoint;
            _player = FindObjectOfType<Player>();
            _levelManager = FindObjectOfType<LevelManager>();

            var waypoint = _currentWaypoint.next;
            while(waypoint != null)
            {
                waypoint.gameObject.SetActive(false);
                waypoint = waypoint.next;
            }
        }

        public void PlayerHitWaypoint(Waypoint waypoint)
        {
            _levelManager.WaypointsHitByPlayer(waypoint);
            _player.MinimumVelocity = waypoint.MinimumVelocity;

            waypoint.Deactivate();
            _currentWaypoint = waypoint.next;

            if (_currentWaypoint == null)
                return;

            _currentWaypoint.gameObject.SetActive(true);
        }
    }
}