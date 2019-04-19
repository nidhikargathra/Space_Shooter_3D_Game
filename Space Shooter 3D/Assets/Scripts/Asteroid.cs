using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Explosion))]
public class Asteroid : MonoBehaviour {
    [SerializeField] float minScale = 2.4f;
    [SerializeField] float maxScale = 6f;
    [SerializeField] float rotationOffset = 50f;

    public static float destructionDelay = 1.0f;

    Transform myT;
    Vector3 randomRotation;

    private void Awake()
    {
        myT = transform;
    }

    private void Start()
    {
        // random size
        Vector3 scale = Vector3.one;
        scale.x = Random.Range(minScale, maxScale);
        scale.y = Random.Range(minScale, maxScale);
        scale.z = Random.Range(minScale, maxScale);

        myT.localScale = scale;

        //random rotation
        randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.y = Random.Range(-rotationOffset, rotationOffset);
        randomRotation.z = Random.Range(-rotationOffset, rotationOffset);

    }
    private void Update ()
    {
        myT.Rotate(randomRotation * Time.deltaTime);	
	}

    public void SelfDestruct()
    {
        float timer = Random.Range(0, destructionDelay);

        Invoke("GoBoom", timer);
    }

    public void GoBoom()
    {
        GetComponent<Explosion>().BlowUp();
    }
}
