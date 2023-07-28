using UnityEngine;

namespace RandomPlacer
{
	public class UniformXYRotationProvider : RotationProvider
	{
		public AngleConstraint XConstraint = new AngleConstraint();
		public AngleConstraint YConstraint = new AngleConstraint();
		
		public override Quaternion GetRotation() =>
				Quaternion.Euler(XConstraint.GetAngle(), YConstraint.GetAngle(), 0);
	}
}