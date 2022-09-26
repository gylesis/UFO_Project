using System.Collections.Generic;
using UnityEngine;

namespace Project.MotherbaseLogic
{
    public class MotherbaseStickService : MonoBehaviour
    {
        [SerializeField] private Transform _stickTransforms;

        private Dictionary<Transform, Transform> _stickedTransforms = new Dictionary<Transform, Transform>();

        public void Unstick(Transform transform)
        {
            if (_stickedTransforms.ContainsKey(transform) == false) return;

            transform.parent = _stickedTransforms[transform];

            _stickedTransforms.Remove(transform);
        }

        public void Stick(Transform transform)
        {
            _stickedTransforms.Add(transform, transform.parent);

            transform.parent = _stickTransforms;
        }
    }
}