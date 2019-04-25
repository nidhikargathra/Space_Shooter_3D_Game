using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    [SerializeField] float rateOfFire;
    [SerializeField] Projectile projectile;

    [HideInInspector]
    public Transform muzzle;

    private float nextFireAllowed;
    public bool canFire;

    private void Awake()
    {
        muzzle = GameObject.FindGameObjectWithTag("Muzzle").transform;   
    }

    public virtual void Fire()
    {
        canFire = false;
        if (Time.time < nextFireAllowed)
            return;
        nextFireAllowed = Time.time + rateOfFire;
        Instantiate(projectile, muzzle.position, muzzle.rotation);

        canFire = true;
    }
}
