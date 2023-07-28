using UnityEngine;

namespace RandomPlacer.Extensions
{
	public static class AxisExtensions
	{
		public static Color ToColor(this Axis axis) =>
				axis switch
				{
						Axis.X => Color.red,
						Axis.Y => Color.green,
						Axis.Z => Color.blue,
						_ => Color.white
				};
		
		public static Vector3 ToVector3(this Axis axis) =>
				axis switch
				{
						Axis.X => Vector3.right,
						Axis.Y => Vector3.up,
						Axis.Z => Vector3.forward,
						_ => Vector3.zero
				};
	}
}
