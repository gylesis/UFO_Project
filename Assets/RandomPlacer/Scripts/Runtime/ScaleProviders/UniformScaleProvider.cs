using UnityEngine;

namespace RandomPlacer.ScaleProviders
{
	public class UniformScaleProvider : ScaleProvider
	{
		public ScaleConstraint Constraint = new();

		public override Vector3 GetScale()
		{
			float scale = Constraint.GetScale();

			return new Vector3(scale, scale, scale);
		}
	}
}