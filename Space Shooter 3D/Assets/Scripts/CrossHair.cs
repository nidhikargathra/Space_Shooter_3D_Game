using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {
    [SerializeField] Texture2D image;
    [SerializeField] int size;
    [SerializeField] float maxAngle;
    [SerializeField] float minAngle;
    [SerializeField] GameObject rocket;

    float lookHeight;
    Transform rocketT;
    float maxDistance;

    private void Start()
    {
        rocketT = rocket.transform;
    }
    public void LookHeight(float value)
    {
        lookHeight += value;
        if (lookHeight > maxAngle || lookHeight < minAngle)
            lookHeight -= value;
    }
    private void Update()
    {
        CastRay();
    }
    void CastRay()
    {
        RaycastHit hit;
        Vector3 fwd = rocketT.TransformDirection(Vector3.forward) * maxDistance;

        if (Physics.Raycast(rocketT.position, fwd, out hit))
        {
            transform.position = hit.point;
        }
    }

    private void OnGUI()
    {
        Vector3 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        screenPosition.y = Screen.height - screenPosition.y;
        GUI.DrawTexture(new Rect(screenPosition.x, screenPosition.y - lookHeight, size, size), image);
    }
}
