using UnityEngine;
using UnityEditor;

namespace Assets.Code
{
    public abstract class BasicWeapon : MonoBehaviour
    {
        public abstract void Fire(Vector3 position, Vector3 direction);
    }
}