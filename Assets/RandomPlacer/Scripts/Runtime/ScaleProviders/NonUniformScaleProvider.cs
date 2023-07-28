using UnityEngine;

namespace RandomPlacer.ScaleProviders
{
	public class NonUniformScaleProvider : ScaleProvider
	{
		public ScaleConstraint XConstraint = new();
		public ScaleConstraint YConstraint = new();
		public ScaleConstraint ZConstraint = new();
		
		public override Vector3 GetScale() =>
				new(XConstraint.GetScale(), YConstraint.GetScale(), ZConstraint.GetScale());
	}
}