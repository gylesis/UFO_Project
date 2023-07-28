using UnityEngine;

namespace RandomPlacer
{
	public class UniformZRotationProvider : RotationProvider
	{
		public AngleConstraint Constraint = new AngleConstraint();
		
		public override Quaternion GetRotation() =>
				Quaternion.Euler(0, 0, Constraint.GetAngle());
	}
}