using UnityEngine;

namespace RandomPlacer
{
	public class UniformXZRotationProvider : RotationProvider
	{
		public AngleConstraint XConstraint = new AngleConstraint();
		public AngleConstraint ZConstraint = new AngleConstraint();
		
		public override Quaternion GetRotation() =>
				Quaternion.Euler(XConstraint.GetAngle(), 0, ZConstraint.GetAngle());
	}
}