using Assets.Code;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Laser laser;

    public GameObject Effect;
    public AudioClip ExplosionClip;

    Vector3 hitPosition;
    private Destroyable _destroyable;
    private LevelManager _levelManager;

    private void Awake()
    {
        _levelManager = FindObjectOfType<LevelManager>();
        _destroyable = GetComponentInParent<Destroyable>();
    }
    private void Update()
    {
        if (!FindTarget())
            return;
        if (InFront() && HaveLineOfSightRaycast())
        {
            //Debug.Log("Fireeee");
            FireLaser();
        }
    }
    bool InFront()
    {
        Vector3 directionToTarget = transform.position - target.position;
        float angle = Vector3.Angle(transform.forward, directionToTarget);

        //if in range
        if (Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
        {
            //Debug.DrawLine(transform.position, target.position, Color.green);
            return true;
        }
        //Debug.DrawLine(transform.position, target.position, Color.yellow);
        return false;
    }

    bool HaveLineOfSightRaycast()
    {
        RaycastHit hit;
        Vector3 direction = target.position - transform.position;

        if (Physics.Raycast(laser.transform.position, direction, out hit, laser.Distance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(laser.transform.position, direction, Color.green);
                hitPosition = hit.transform.position;
                return true;
            }
        }
        return false;
    }

    void FireLaser()
    {
        laser.FireLaser(hitPosition, target);
    }

    bool FindTarget()
    {
        if (target == null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("Player");
            if (temp != null)
                target = temp.transform;
        }

        if (target == null)
            return false;

        return true;
    }
    public void Destroyed(GameObject from)
    {
        var source = GameExtensions.PlayClipAtPoint(transform.position, ExplosionClip);
        source.rolloffMode = AudioRolloffMode.Linear;
        _levelManager.EnemyDestroyedByPlayer();
        //Instantiate(Effect, transform.position, transform.rotation);
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("on triggerin enemy by: "+other.name);
        var shield = other.GetComponentInParent<Shield>();
        if (shield == null)
            return;
        shield.TakeDamage(50 / 3);

        _destroyable.TakeDamage(_destroyable.Health/4, other.gameObject);

    }
    public void TookDamage(int damage)
    {
        //Debug.Log("enemy in tookdamage");
        var shield = GetComponentInParent<Shield>();
        if (shield == null)
            return;
        shield.TakeDamage(damage);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log("enemy on collision by " + collision.gameObject.name);

    //    var shield = collision.gameObject.GetComponentInParent<Shield>();
    //    if (shield == null)
    //        return;
    //    shield.TakeDamage(50 / 3);

    //    _destroyable.TakeDamage(_destroyable.Health, collision.gameObject.gameObject);

    //}
}
