using UnityEngine;

namespace RandomPlacer
{
	public class SpherePositionProvider : PositionProvider
	{
		[field:SerializeField] public float Radius { get; set; } = 1f;
		[field:SerializeField] public Vector3 Center { get; set; } = Vector3.zero;
		
		public override Vector3 GetPosition() =>
				transform.position + Random.insideUnitSphere * Radius + Center;
	}
}
