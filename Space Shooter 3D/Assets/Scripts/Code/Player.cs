﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Player : MonoBehaviour
    {
        private Camera camera;
        public BasicWeapon basicWeapon;


        private PlayerCamera _camera;
        private PlayerController _controller;
        private PlayerGUI _playerGUI;
        private PlayerWeapons _weapons;

        private IEnumerable<BasicWeaponMount> _mounts;

        private void Awake()
        {
            camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            _mounts = GetComponentsInChildren<BasicWeaponMount>();

            _camera = new PlayerCamera(this, camera);
            _controller = new PlayerController(this);
            _playerGUI = new PlayerGUI(_controller, this);
            _weapons = new PlayerWeapons(this, camera, _controller, _mounts);

            Equip(basicWeapon);
        }

        public void Equip(BasicWeapon weapon)
        {
            foreach (var mount in _mounts)
                mount.Equip(weapon);
        }

        private void Update()
        {
            //order matters, camera follows controller
            _controller.Update();
            _camera.Update();
            _weapons.Update();
        }
        private void OnGUI()
        {
            _playerGUI.OnGUI();
        }
    }
}