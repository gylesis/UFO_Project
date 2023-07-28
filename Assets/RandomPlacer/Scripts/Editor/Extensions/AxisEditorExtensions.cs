using UnityEditor.IMGUI.Controls;

namespace RandomPlacer.Extensions
{
	public static class AxisEditorExtensions
	{
		public static PrimitiveBoundsHandle.Axes ToAxes(this Axis axis) =>
				axis switch
				{
						Axis.X => PrimitiveBoundsHandle.Axes.Y | PrimitiveBoundsHandle.Axes.Z,
						Axis.Y => PrimitiveBoundsHandle.Axes.X | PrimitiveBoundsHandle.Axes.Z,
						Axis.Z => PrimitiveBoundsHandle.Axes.X | PrimitiveBoundsHandle.Axes.Y,
						_ => PrimitiveBoundsHandle.Axes.None
				};
	}
}
