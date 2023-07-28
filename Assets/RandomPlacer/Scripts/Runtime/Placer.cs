using RandomPlacer.ScaleProviders;

using UnityEngine;

namespace RandomPlacer
{
	public class Placer : MonoBehaviour
	{
		public BoundsType BoundsType;
		public RotationType RotationType;
		public ScaleType ScaleType;
		public PrefabProvider PrefabProvider;

		public GameObject Place()
		{
			GameObject prefab = PrefabProvider.GetPrefab();

			Vector3 position = GetComponent<PositionProvider>().GetPosition();
			Quaternion rotation = GetComponent<RotationProvider>().GetRotation();
			Vector3 scale = GetComponent<ScaleProvider>().GetScale();

			GameObject instantiate = Instantiate(prefab, position,rotation, transform);
			instantiate.transform.localScale = scale;
			return instantiate;
		}
		
		public void Clear()
		{
			while (transform.childCount > 0)
				DestroyImmediate(transform.GetChild(0).gameObject);
		}
		
		public GameObject Replace()
		{
			Clear();
			return Place();
		}
	}
}
