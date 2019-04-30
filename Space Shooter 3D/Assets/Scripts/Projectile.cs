using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] int damage;

    private void Start()
    {
        print("start missle");
        Destroy(gameObject, timeToLive);
    }
    private void Update()
    {
        //print("in update");
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Shield targetShield = other.transform.GetComponentInParent<Shield>();
        if (targetShield == null)
            return;
        targetShield.TakeDamage(damage);
        Destroy(gameObject);
    }
}
