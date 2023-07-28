using UnityEngine;

namespace RandomPlacer.PrefabsProviders
{
	using Utilities;

	[CreateAssetMenu(menuName = "Random Placer/Prefabs Providers/Collection")]
	public class PrefabCollectionProvider : PrefabProvider
	{
		[SerializeField] private GameObject[] _prefabs;

		private RandomStack<GameObject> _stack;

		private RandomStack<GameObject> Stack => _stack ??= new RandomStack<GameObject>(_prefabs);

		public override GameObject GetPrefab() =>
				Stack.GetNext();
	}
}
