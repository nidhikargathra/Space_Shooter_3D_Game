using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] Vector3 defaultDistance = new Vector3(0f, 2f, -5f);
    [SerializeField] float distanceDamp = 0.2f;
//    [SerializeField] float rotationalDamp = 5f;

    Transform myT;
    public Vector3 Velocity = Vector3.one;

    private void Awake()
    {
        myT = transform;
    }

    private void LateUpdate()
    {
        if (!FindTarget())
            return;
        SmoothFollow();
        //Vector3 toPos = target.position + (target.rotation * defaultDistance);
        //Vector3 curPos = Vector3.Lerp(myT.position, toPos, distanceDamp * Time.deltaTime);
        //myT.position = curPos;

        //Quaternion toRot = Quaternion.LookRotation(target.position - myT.position, target.up);
        //Quaternion curRot = Quaternion.Slerp(myT.rotation, toRot, rotationalDamp * Time.deltaTime);
        //myT.rotation = curRot;
    }

    void SmoothFollow()
    {
        //Debug.Log("camera follow playership");
        //follow player ship
        Vector3 toPos = target.position + (target.rotation * defaultDistance);
        Vector3 curPos = Vector3.SmoothDamp(myT.position, toPos, ref Velocity,distanceDamp);
        myT.position = curPos;

        //rotate with player ship
        myT.LookAt(target, target.up);

    }

    bool FindTarget()
    {
        if (target == null)
        {
            GameObject temp = GameObject.FindWithTag("Player");
            if (temp != null)
            {
                Debug.Log("Found player: "+temp.name);

                target = temp.transform;
            }
        }

        if (target == null)
            return false;

        return true;
    }
}
