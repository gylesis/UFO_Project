using UnityEngine;

namespace RandomPlacer.PrefabsProviders
{
	[CreateAssetMenu(menuName = "Random Placer/Prefabs Providers/One")]
	public class OnePrefabProvider : PrefabProvider
	{
		[SerializeField] private GameObject _prefab;

		
		public override GameObject GetPrefab() =>
				_prefab;
	}
}
