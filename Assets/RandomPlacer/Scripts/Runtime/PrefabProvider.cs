using UnityEngine;

namespace RandomPlacer
{
	public abstract class PrefabProvider : ScriptableObject
	{
		public abstract GameObject GetPrefab();
	}
}
