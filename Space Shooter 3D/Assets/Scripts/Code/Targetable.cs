using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Targetable : MonoBehaviour
    {
        private const float BaseOffScreenSize = 16;
        private const float BaseMaxSize = 128;
        private const float BaseMinSize = 100;
        private const float SmoothMovement = 25f;

        private Vector3
            _screenPosition,
            _currentPositon,
            _offScreenPosition,
            _targetPosition;

        private float _currentSize, _targetSize;

        private bool _isOffScreen;

        private void Update()
        {
            _isOffScreen = false;
            _offScreenPosition = _screenPosition = Camera.main.WorldToScreenPoint(transform.position);
            var distance = Vector3.Distance(Camera.main.transform.position, transform.position);

            _targetSize = Mathf.Clamp(BaseMaxSize / distance, BaseMinSize, BaseMaxSize);

            _targetPosition.x = _screenPosition.x - _currentSize / 2;
            _targetPosition.y = Screen.height - _screenPosition.y - _currentSize / 2;

            _offScreenPosition.x = _screenPosition.x - BaseOffScreenSize / 2f;
            _offScreenPosition.y = Screen.height - _screenPosition.y - BaseOffScreenSize / 2f;

            if (_screenPosition.z < 0)
            {
                _isOffScreen = true;
                _offScreenPosition.x =
                    _screenPosition.x < Screen.width / 2f
                    ? BaseOffScreenSize / 2f
                    : Screen.width - BaseOffScreenSize * 2;

                _offScreenPosition.y =
                    _screenPosition.y < Screen.height / 2f
                    ? BaseOffScreenSize / 2f
                    : Screen.height - BaseOffScreenSize * 2;
            }
            if(_offScreenPosition.x < 0)
            {
                _isOffScreen = true;
                _offScreenPosition.x = BaseOffScreenSize / 2;
            }
            else if(_offScreenPosition.x > Screen.width - BaseOffScreenSize)
            {
                _isOffScreen = true;
                _offScreenPosition.x = Screen.width - BaseOffScreenSize * 2;
            }
                
            if(_offScreenPosition.y < 0)
            {
                _isOffScreen = true;
                _offScreenPosition.y = BaseOffScreenSize / 2;
            }
            else if (_offScreenPosition.y > Screen.height - BaseOffScreenSize)
            {
                _isOffScreen = true;
                _offScreenPosition.y = Screen.height - BaseOffScreenSize * 2;
            }

            _currentSize = Mathf.Lerp(_currentSize, _targetSize, Time.deltaTime * 4);
            _currentPositon.x = Mathf.Lerp(_currentPositon.x, _targetPosition.x, SmoothMovement * Time.deltaTime);
            _currentPositon.y = Mathf.Lerp(_currentPositon.y, _targetPosition.y, SmoothMovement * Time.deltaTime);
            

        }
        private void OnGUI()
        {
            var oldColor = GUI.color;
            GUI.color = Color.yellow;

            if (_isOffScreen)
                GUI.DrawTexture(
                    new Rect(_offScreenPosition.x, _offScreenPosition.y, BaseOffScreenSize, BaseOffScreenSize),
                    PlayerGUI.OffscreenIndicatorImage);
            else
                GUI.DrawTexture(
                    new Rect(_currentPositon.x, _currentPositon.y, _currentSize, _currentSize),
                    PlayerGUI.TargetSquareImage);

            GUI.color = oldColor;
        }
    }
}