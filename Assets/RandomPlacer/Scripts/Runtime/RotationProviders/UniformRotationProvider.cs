using UnityEngine;

namespace RandomPlacer
{
	public class UniformRotationProvider : RotationProvider
	{
		public AngleConstraint XAngleConstraint = new AngleConstraint();
		public AngleConstraint YAngleConstraint = new AngleConstraint();
		public AngleConstraint ZAngleConstraint = new AngleConstraint();

		public override Quaternion GetRotation()
		{
			float x = XAngleConstraint.GetAngle();
			float y = YAngleConstraint.GetAngle();
			float z = ZAngleConstraint.GetAngle();

			return Quaternion.Euler(x, y, z);
		}
	}
}
