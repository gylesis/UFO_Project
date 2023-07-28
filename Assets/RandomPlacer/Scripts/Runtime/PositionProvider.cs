using UnityEngine;

namespace RandomPlacer
{
	public abstract class PositionProvider : MonoBehaviour
	{
		public abstract Vector3 GetPosition();
	}
}
