using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(EnemyAttack))]
[RequireComponent(typeof(TrailRenderer))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float movementSpeed = 25f;
    [SerializeField] float rotationalDamp = 1.25f;
    [SerializeField] float raycastOffset = 2.5f;
    [SerializeField] float detectionDistance = 20f;

    private void OnEnable()
    {
        EventManager.onPlayerDeath += FindMainCamera;
        EventManager.onStartGame += SelfDestruct;
    }

    private void OnDisable()
    {
        EventManager.onPlayerDeath -= FindMainCamera;
        EventManager.onStartGame -= SelfDestruct;
    }

    void SelfDestruct()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (!FindTarget())
            return;
        PathFinding();
        Move();
    }

    void Turn()
    {
        Vector3 pos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(pos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationalDamp * Time.deltaTime);
            
    }

    void Move()
    {
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
    }

    void PathFinding()
    {
        RaycastHit hit;
        Vector3 moveOffset = Vector3.zero;

        Vector3 left = transform.position - transform.right * raycastOffset;
        Vector3 right = transform.position + transform.right * raycastOffset;
        Vector3 up = transform.position + transform.up * raycastOffset;
        Vector3 down = transform.position - transform.up * raycastOffset;

        Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
        Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

        if(Physics.Raycast(left, transform.forward,out hit,detectionDistance))
            moveOffset += Vector3.right;
        else if (Physics.Raycast(right, transform.forward, out hit, detectionDistance))
            moveOffset -= Vector3.right;

        if (Physics.Raycast(up, transform.forward, out hit, detectionDistance))
            moveOffset -= Vector3.up;
        else if (Physics.Raycast(down, transform.forward, out hit, detectionDistance))
            moveOffset += Vector3.up;

        if (moveOffset != Vector3.zero)
            transform.Rotate(moveOffset * 5f * Time.deltaTime);
        else
            Turn();
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

    void FindMainCamera()
    {
        target = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }
}
