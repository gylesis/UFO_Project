using UnityEngine;

namespace RandomPlacer
{
	public class PointPositionProvider : PositionProvider
	{
		public override Vector3 GetPosition() => transform.position;
	}
}