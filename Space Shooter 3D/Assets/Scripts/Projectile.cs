using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeToLive;
    [SerializeField] float damage;

    private void Start()
    {
        Destroy(gameObject, timeToLive);
    }
    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Shield targetShield = other.transform.GetComponentInParent<Shield>();
        if (targetShield == null)
            return;
        targetShield.TakeDamage();
    }
}
