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
            Debug.Log("Player hit me: "+transform.name);

            EventManager.ScorePoints(points);
            switch (transform.name)
            {
                case "Battery":
                    {
                        EventManager.ReSpawnPickup();
                        break;
                    }
                case "Orb":
                    {

                        break;
                    }
            }
            Destroy(gameObject);
        }
    }
}
