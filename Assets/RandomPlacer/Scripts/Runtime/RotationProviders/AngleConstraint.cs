using System;

using UnityEngine;

using Random = UnityEngine.Random;

namespace RandomPlacer
{
	[Serializable]
	public class AngleConstraint
	{
		[Range(0,360)]
		public float Min = 0;
		[Range(0,360)]
		public float Max = 360;
		
		public float GetAngle() =>
				Random.Range(Min, Max);
	}
}