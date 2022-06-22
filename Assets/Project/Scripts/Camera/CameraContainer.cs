using UnityEngine;

namespace Project.Scripts.Camera
{
    public class CameraContainer : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;
        [SerializeField] private Transform _camParent;

        public UnityEngine.Camera Camera => _camera;
        public Transform CamParent => _camParent;
    }
}