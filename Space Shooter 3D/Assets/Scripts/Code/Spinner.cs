using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    public class Spinner : MonoBehaviour
    {
        public bool Direction;

        private void Update()
        {
            transform.Rotate(0, 0, Time.deltaTime * 20 * (Direction ? 1 : -1) , Space.Self);
        }
    }
}