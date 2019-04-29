using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Player : MonoBehaviour
    {
        public Camera camera;

        public PlayerCamera _camera;
        public PlayerController _controller;
        public PlayerGUI _playerGUI;

        private void Awake()
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            _camera = new PlayerCamera(this, camera);
            _controller = new PlayerController(this);
            _playerGUI = new PlayerGUI(_controller, this);
        }

        private void Update()
        {
            //order matters, camera follows controller
            _controller.Update();
            _camera.Update();
        }
        private void OnGUI()
        {
            _playerGUI.OnGUI();
        }
    }
}
