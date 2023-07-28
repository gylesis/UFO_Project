using UnityEngine;

namespace RandomPlacer
{
	public class UniformYRotationProvider : RotationProvider
	{
		public AngleConstraint Constraint = new AngleConstraint();
		
		public override Quaternion GetRotation() =>
				Quaternion.Euler(0, Constraint.GetAngle(), 0);
	}
}