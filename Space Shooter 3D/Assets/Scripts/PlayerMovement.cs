using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour {

    [System.Serializable]
    public class MouseInput
    {
        public Vector2 damping = new Vector2(1,1);
        public Vector2 sensitivity = new Vector2(1,1);
    }
    [SerializeField] float mouseSpeed = 5;
    [SerializeField] MouseInput mouseControl;

    [SerializeField] float movementSpeed = 50f;
    [SerializeField] float turnSpeed = 60f;
    [SerializeField] Thruster[] thruster;

    Transform myT;
    Vector2 mouseInput;
    Vector2 playerInput;

    void Awake()
    {
        myT = transform;
    }
    void Update()
    {
        Thrust();
        Turn();
        MouseMovement();
    }

    void MouseMovement()
    {
        playerInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseInput.x = Mathf.Lerp(mouseInput.x, playerInput.x, 1f / mouseControl.damping.x);
        mouseInput.y = Mathf.Lerp(mouseInput.y, playerInput.y, 1f / mouseControl.damping.y);

        myT.Rotate(Vector3.up * mouseInput.x * mouseControl.sensitivity.x);
        myT.Rotate(Vector3.left * mouseInput.y * mouseControl.sensitivity.y);
    }
    void Turn()
    {
        float yaw = turnSpeed * Time.deltaTime * Input.GetAxis("Horizontal");
        float pitch = turnSpeed * Time.deltaTime * Input.GetAxis("Pitch");
        float roll = turnSpeed * Time.deltaTime * Input.GetAxis("Roll");

        myT.Rotate(pitch, yaw, -roll);
    }
    void Thrust()
    {
        //if we start to thrust, call thruster.activate, to stop call thruster.activate(false)

        if (Input.GetAxis("Vertical") > 0)
        {
            myT.position += myT.forward * movementSpeed * Time.deltaTime * Input.GetAxis("Vertical");

            foreach (Thruster t in thruster)
                t.Intensity(Input.GetAxis("Vertical"));
        }
    }
}
