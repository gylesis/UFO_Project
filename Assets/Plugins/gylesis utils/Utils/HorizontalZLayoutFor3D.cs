using System.Collections.Generic;
using UnityEngine;

namespace Warlords.Utils
{
    [ExecuteInEditMode]
    public class HorizontalZLayoutFor3D : MonoBehaviour
    {
        [SerializeField] private float _offset;
        [SerializeField] private List<Transform> _objects;

        private void OnTransformChildrenChanged()
        {
            _objects?.Clear();

            _objects = new List<Transform>();
            var childCount = transform.childCount;

            for (int i = 0; i < childCount - 1; i++)
            {
                Transform child = transform.GetChild(i);
                _objects.Add(child);
            }
        }
        
        [ContextMenu(nameof(ResetPoses))]
        private void ResetPoses()
        {
            foreach (Transform obj in _objects)
            {
                obj.localPosition = Vector3.zero;
            }
        }

        private void OnValidate()
        {
            for (var index = 1; index < _objects.Count; index++)
            {
                Transform trnsform = _objects[index];

                Vector3 trnsformLocalPosition = _objects[index - 1].localPosition;

                trnsformLocalPosition.x = trnsform.localPosition.x;
                
               // trnsformLocalPosition.x += trnsform.localScale.x + _offset.x;
                trnsformLocalPosition.z += trnsform.localScale.z + _offset;
                
                trnsform.localPosition = trnsformLocalPosition;
            }
        }
    }
}