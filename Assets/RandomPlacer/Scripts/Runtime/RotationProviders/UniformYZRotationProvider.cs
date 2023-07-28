using UnityEngine;

namespace RandomPlacer
{
	public class UniformYZRotationProvider : RotationProvider
	{
		public AngleConstraint YConstraint = new AngleConstraint();
		public AngleConstraint ZConstraint = new AngleConstraint();
		
		public override Quaternion GetRotation() =>
				Quaternion.Euler(0, YConstraint.GetAngle(), ZConstraint.GetAngle());
	}
}