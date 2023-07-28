using UnityEngine;

namespace RandomPlacer
{
	public class UniformXRotationProvider : RotationProvider
	{
		public AngleConstraint Constraint = new AngleConstraint();

		
		public override Quaternion GetRotation()
		{
			float x = Constraint.GetAngle();
			return Quaternion.Euler(x, 0, 0);
		}
	}
}