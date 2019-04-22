using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour {
    [SerializeField] float rotationOffset = 50f;

    static int points = 100;
    Vector3 randomRotation;
    Transform myT;

    private void Awake()
    {
        myT = transform;
    }

    private void Start()
    {
        //random rotation
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);
    }
    private void Update()
    {
        myT.Rotate(randomRotation * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Player"))
            PickupHit();
    }

    public void PickupHit()
    {
        Debug.Log("Player hit us");
        EventManager.ScorePoints(points);
        EventManager.ReSpawnPickup();
        Destroy(gameObject);
    }
}
