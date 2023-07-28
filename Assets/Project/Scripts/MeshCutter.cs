using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project
{
    public class MeshCutter : MonoBehaviour
    {
        [SerializeField] private Transform _firstPoint;
        [SerializeField] private Transform _secondPoint;

        [SerializeField] private SpriteRenderer _cutMesh;

        [Range(-1f, 1f)] [SerializeField] private float _threshold = 0.5f;

        [SerializeField] private bool _active;

        [SerializeField] private float _debugSphereRadius = 0.1f;
        [SerializeField] private float _verticalOffset = 1f;

        [SerializeField] private Material _defaultMaterial;
        
        
        private Vector3 _maxYPoint;
        private Vector3 _minYPoint;
        private Vector3 _maxXPoint;
        private Vector3 _minXPoint;


        private void OnDrawGizmos()
        {
            if (_active == false) return;

            Gizmos.DrawLine(_firstPoint.position, _secondPoint.position);

            var meshVertices = _cutMesh.sprite.vertices;

            Vector3 cutDirection = (_secondPoint.position - _firstPoint.position).normalized;

            foreach (Vector3 vertex in meshVertices)
            {
                Vector3 worldVertex = _cutMesh.transform.TransformPoint(vertex);

                if (IsLeft(_firstPoint.position, _secondPoint.position, worldVertex))
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(worldVertex, _debugSphereRadius);
                }
                else
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawSphere(worldVertex, _debugSphereRadius);
                }
            }
            
            Gizmos.color = Color.green;
        
            Gizmos.DrawSphere(_maxYPoint, _debugSphereRadius);
            Gizmos.DrawSphere(_minYPoint, _debugSphereRadius);
            Gizmos.DrawSphere(_maxXPoint, _debugSphereRadius);
            Gizmos.DrawSphere(_minXPoint, _debugSphereRadius);
        }

        [ContextMenu(nameof(CutSprite))]
        private void CutSprite()
        {
            //Cut(_cutMesh, _firstPoint.position, _secondPoint.position);
        }

        private void Cut(SpriteRenderer spriteRenderer, Vector3 firstPoint, Vector3 secondPoint)
        {
            // Vector3 cutDirection = (_secondPoint.position - _firstPoint.position).normalized;

            var spriteToCut = spriteRenderer.sprite;
            var ySpriteSize = spriteToCut.rect.height / 2;
            
            var meshVertices = spriteToCut.vertices;

            List<Vector3> leftPoints = new List<Vector3>(32);
            List<Vector3> rightPoints = new List<Vector3>(32);

            foreach (Vector3 vertex in meshVertices)
            {
                Vector3 worldVertex = spriteRenderer.transform.TransformPoint(vertex);

                var isLeft = IsLeft(firstPoint, secondPoint, worldVertex);

                if (isLeft)
                {
                    leftPoints.Add(worldVertex);
                }
                else
                {
                    rightPoints.Add(worldVertex);
                }
            }

            var leftSprite = new GameObject("Left Part");
            var rightSprite = new GameObject("Right Part");

            var upperBodyRenderer = leftSprite.AddComponent<SpriteRenderer>();
            upperBodyRenderer.sprite = CreateSprite(leftPoints);
            upperBodyRenderer.material = spriteRenderer.material;

            var lowerBodyRenderer = rightSprite.AddComponent<SpriteRenderer>();
            lowerBodyRenderer.sprite = CreateSprite(rightPoints);
            lowerBodyRenderer.material = spriteRenderer.material;


            Sprite CreateSprite(List<Vector3> points)
            {
                _maxYPoint = points.OrderByDescending(x => x.y).First();
                _minYPoint = points.OrderByDescending(x => x.y).Last();

                _maxXPoint = points.OrderByDescending(x => x.x).First();
                _minXPoint = points.OrderByDescending(x => x.x).Last();

                float height = _maxYPoint.y - _minYPoint.y;

                height *= spriteToCut.pixelsPerUnit;
                
                var xAverage = points.Sum(x => x.x) / points.Count;
                var yAverage = points.Sum(x => x.y) / points.Count;

                //Vector2 pivot = new Vector2(xAverage, yAverage);
                Vector2 pivot = Vector2.zero;
                
                var rect = Rect.MinMaxRect(_minXPoint.x * spriteToCut.pixelsPerUnit, 
                    _minYPoint.y * spriteToCut.pixelsPerUnit, _maxXPoint.x * spriteToCut.pixelsPerUnit, _maxYPoint.y  * spriteToCut.pixelsPerUnit);

                rect.height = height;
                
                Sprite sprite = Sprite.Create(
                    spriteToCut.texture,
                    rect,
                    pivot,
                    spriteToCut.pixelsPerUnit,
                    0,
                    SpriteMeshType.FullRect);

                return sprite;
            }
            
        }


        [ContextMenu(nameof(CreateMesh))]
        private void CreateMesh()
        {
            return;
            var mesh = new Mesh();

            mesh.vertices = _cutMesh.sprite.vertices.Select(x => new Vector3(x.x, x.y, 0)).ToArray();
            int[] triangles = new int[3 * (mesh.vertices.Length - 2)];
            
            /*
             
            0,1,2
            1,3,2
            2,3,4
            
            */

            int vertexIndex = 0;
            int triangleIndex = 0;

            for (int i = 1; i < mesh.vertices.Length - 1; i++)
            {
                triangles[triangleIndex++] = vertexIndex;
                triangles[triangleIndex++] = vertexIndex + i;
                triangles[triangleIndex++] = vertexIndex + i + 1;
                
                vertexIndex++;
            }

            mesh.triangles = triangles.ToArray();
            
            var meshObject = new GameObject("MeshObject");

            var meshFilter = meshObject.AddComponent<MeshFilter>();
            meshFilter.mesh = mesh;
            var meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshRenderer.material = _defaultMaterial;
        }
        
        public bool IsLeft(Vector3 a, Vector3 b, Vector3 c)
        {
            return (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x) > 0;
        }

        public bool IsRight(Vector3 a, Vector3 b, Vector3 c)
        {
            return (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x) < 0;
        }
    }
}