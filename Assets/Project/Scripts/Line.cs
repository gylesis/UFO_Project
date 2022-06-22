using UnityEngine;

namespace Project.Scripts
{
    public class Line : MonoBehaviour
    {
        [SerializeField] private LineMarker _lineMarker;

        
        [ContextMenu("Draw")]
        private void Execute()
        {
            var scaleY = transform.localScale.y;

            for (int i = 0; i <= scaleY; i += 5)
            {
                Vector2 pos = transform.position;
                pos.y = i;
                
                LineMarker lineMarker = Instantiate(_lineMarker, pos,Quaternion.identity,transform);

                if (i % 10 == 0)
                {
                    Vector3 transformLocalScale = lineMarker.transform.localScale;
                    transformLocalScale *= 1.5f;
                    lineMarker.transform.localScale = transformLocalScale;
                }
                lineMarker.Text.text = $"{i}m";
            }
        }
    }
}