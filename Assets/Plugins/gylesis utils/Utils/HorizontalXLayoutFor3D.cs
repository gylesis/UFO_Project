using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Warlords.Utils
{
    [ExecuteInEditMode]
    public class HorizontalXLayoutFor3D : MonoBehaviour
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
            if(_objects == null) return;
            
            for (var index = 1; index < _objects.Count; index++)
            {
                Transform trnsform = _objects[index];

                if(trnsform == null) continue;
                
                Vector3 trnsformLocalPosition = _objects[index - 1].localPosition;

                trnsformLocalPosition.x += trnsform.localScale.x + _offset;
                
                trnsform.localPosition = trnsformLocalPosition;
            }
        }
    }
}