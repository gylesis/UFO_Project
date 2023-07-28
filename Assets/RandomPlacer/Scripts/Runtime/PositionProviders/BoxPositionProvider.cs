using UnityEngine;

namespace RandomPlacer
{
	public class BoxPositionProvider : PositionProvider
	{
		[field:SerializeField] public Vector3 Size { get; set; } = Vector3.one;
		[field:SerializeField] public Vector3 Center { get; set; } = Vector3.zero;
		
		public override Vector3 GetPosition()
		{
			float x = Random.Range(-Size.x / 2, Size.x / 2);
			float y = Random.Range(-Size.y / 2, Size.y / 2);
			float z = Random.Range(-Size.z / 2, Size.z / 2);
			
			return transform.position + new Vector3(x, y, z) + Center;
		}
	}
}