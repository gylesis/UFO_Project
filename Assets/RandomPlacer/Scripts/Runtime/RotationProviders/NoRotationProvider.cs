using UnityEngine;

namespace RandomPlacer
{
	public class NoRotationProvider : RotationProvider
	{
		public override Quaternion GetRotation() =>
				Quaternion.identity;
	}
}