using System;

using Random = UnityEngine.Random;

namespace RandomPlacer.ScaleProviders
{
	[Serializable]
	public class ScaleConstraint
	{
		public float Min = 1;
		public float Max = 1;

		public float GetScale() =>
				Random.Range(Min, Max);
	}
}
