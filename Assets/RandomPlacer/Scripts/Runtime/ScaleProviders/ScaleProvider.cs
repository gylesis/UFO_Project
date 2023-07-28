using UnityEngine;

namespace RandomPlacer.ScaleProviders
{
	public abstract class ScaleProvider : MonoBehaviour
	{
		public abstract Vector3 GetScale();
	}
}
