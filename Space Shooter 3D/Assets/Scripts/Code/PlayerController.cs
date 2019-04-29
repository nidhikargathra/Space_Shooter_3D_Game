using System;
using UnityEngine;

namespace Assets.Code
{
    public class PlayerController
    {
        private readonly Player _player;

        private float
            _baseVelocity,
            _targetVelocity,
            _variableVelocity;  

        public Vector3 MousePosition { get; private set; }
        public float CurrentVelocity { get; private set; }
        public float MaxVariableVelocity { get; set; }
        public float MinimumVelocity { get; set; }
        public float Acceleration { get; set; }
        public float VelocityDamp { get; set; }
        public float RotationSpeed { get; set; }
        public bool UseRelativeMovement { get; set; }
        public Vector2 MouseSensitivity { get; set; }
        public float AfterBurnerModifier { get; set; }
        public float StrafeModifier { get; set; }

        public PlayerController(Player player)
        {
            MaxVariableVelocity = 20f;
            Acceleration = 70f;
            VelocityDamp = 20f;
            RotationSpeed = 0.01f;
            MouseSensitivity = new Vector2(700, 700);
            UseRelativeMovement = false;
            AfterBurnerModifier = 50;
            StrafeModifier = 7;

            _player = player;
        }

        public void Update()
        {
            Cursor.lockState = CursorLockMode.Confined;
            if (UseRelativeMovement)
            {
                MousePosition += new Vector3(
                    Input.GetAxis("Mouse X") * Time.deltaTime * MouseSensitivity.x,
                    Input.GetAxis("Mouse Y") * Time.deltaTime * MouseSensitivity.y
                    ); 
            }
            else
            {
                MousePosition = Input.mousePosition;
            }

            UpdatePosition();
            UpdateRotation();
        }

        private void UpdatePosition()
        {
            _variableVelocity = Mathf.Clamp(
                _variableVelocity + Input.GetAxis("Vertical") * Time.deltaTime * Acceleration,
                0,
                MaxVariableVelocity
                );

            _targetVelocity = _variableVelocity + MinimumVelocity;

            if (Input.GetKey(KeyCode.Tab))
                _targetVelocity += AfterBurnerModifier; 

            CurrentVelocity = Mathf.Lerp(CurrentVelocity, _targetVelocity, Time.deltaTime * VelocityDamp);

            _player.transform.Translate(
                Input.GetAxis("Horizontal") * Time.deltaTime * StrafeModifier,
                0,
                CurrentVelocity * Time.deltaTime,
                Space.Self
                );
        }

        private void UpdateRotation()
        {
            if (Input.GetKey("e"))
                _player.transform.Rotate(0, 0, -90f * Time.deltaTime);

            if (Input.GetKey("q"))
                _player.transform.Rotate(0, 0, 90f * Time.deltaTime);

            var MouseMovement = (MousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f)) * 0.2f;

            if (MouseMovement.sqrMagnitude >= 1)
                _player.transform.Rotate(new Vector3(-MouseMovement.y, MouseMovement.x, 0) * RotationSpeed);
        }
    }
}
