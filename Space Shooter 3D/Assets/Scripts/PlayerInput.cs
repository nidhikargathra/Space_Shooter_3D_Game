﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerInput : MonoBehaviour {
    [SerializeField] Laser[] laser;
    [SerializeField] Shooter rocket;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Laser l in laser)
            {
                //Vector3 pos = transform.position + (transform.forward * l.Distance);
                l.FireLaser();
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            rocket.Fire();
        }
    }
}
