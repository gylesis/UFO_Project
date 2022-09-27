
using UnityEngine;

namespace Project.CameraLogic
{
    public class CameraContainer : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Transform _camParent;

        public Camera Camera => _camera;
        public Transform CamParent => _camParent;
    }
}   