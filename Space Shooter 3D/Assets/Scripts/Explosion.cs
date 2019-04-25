using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Explosion : MonoBehaviour {
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject blowUp;
    [SerializeField] Rigidbody rigidBody;
    [SerializeField] Shield shield;
    [SerializeField] float laserHitModifier = 10f;

    private void IveBeenHit(Vector3 pos)
    {
        GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform) as GameObject;
        Destroy(go, 6f);

        if (shield == null)
            return;

        shield.TakeDamage();
    }

    private void OnCollisionEnter(Collision collision)
    {
        print("I collided with: " + collision.gameObject.name);
        foreach (ContactPoint contact in collision.contacts)
            IveBeenHit(contact.point);
    }
    private void OnTriggerEnter(Collider other)
    {
        IveBeenHit(other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
    }
    public void AddForce(Vector3 hitPosition, Transform hitSource)
    {
        IveBeenHit(hitPosition);
        //Debug.LogWarning("AddForce Called: "+ gameObject.name + " >> " + hitSource.name);
        if (rigidBody == null)
            return;

        Vector3 forceVector = (hitSource.position - hitPosition).normalized;
        //Debug.Log(forceVector * laserHitModifier);
        rigidBody.AddForceAtPosition(-forceVector * laserHitModifier, hitPosition, ForceMode.Impulse);
    }

    public void BlowUp()
    {
        GameObject temp = Instantiate(blowUp, transform.position, Quaternion.identity) as GameObject;

        Destroy(temp, 3f); //destroy explosion particle effects

        Destroy(gameObject); // destroy self
    }
}
