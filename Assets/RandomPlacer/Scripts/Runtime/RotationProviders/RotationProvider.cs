using UnityEngine;

namespace RandomPlacer
{
	public abstract class RotationProvider : MonoBehaviour
	{
		public abstract Quaternion GetRotation();
	}
}
