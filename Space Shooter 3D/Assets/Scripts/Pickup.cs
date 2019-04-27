using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class Pickup : MonoBehaviour {
    [SerializeField] float rotationOffset = 50f;
    [SerializeField] int points = 100;
    bool gotHit = false;

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
        if(!gotHit)
        {
            gotHit = true;
            string pickupName = transform.name.ToString();
            Debug.Log("Player hit me: " + pickupName);

            EventManager.ScorePoints(points);

            if (pickupName.Contains("Battery"))
                EventManager.ReSpawnPickup();
            else if (pickupName.Contains("Orb"))
                EventManager.CollectOrbs();
            
            Destroy(gameObject);
        }
    }
}
