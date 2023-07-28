using System;
using UnityEngine;

namespace Project
{
    [ExecuteInEditMode]
    public class Object2DRotator : MonoBehaviour
    {
        [SerializeField] private Vector2 _eulerDelta = new Vector2(1,0);

        private void Update()
        {
            Vector3 eulerAngles = transform.rotation.eulerAngles;

            eulerAngles.x += _eulerDelta.x;
            eulerAngles.z += _eulerDelta.y;
            
            transform.rotation = Quaternion.Euler(eulerAngles);
        }
    }
}