using System.Collections;
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
                l.FireLaser();
            }
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            print("hit fire key");
            rocket.Fire();
        }
    }
}
