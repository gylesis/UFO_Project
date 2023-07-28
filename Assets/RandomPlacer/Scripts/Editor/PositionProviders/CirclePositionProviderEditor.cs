using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEditor.IMGUI.Controls;

using UnityEngine;

namespace RandomPlacer.PositionProviders
{
	using Extensions;

	[CustomEditor(typeof(CirclePositionProvider))]
	[CanEditMultipleObjects]
	public class CirclePositionProviderEditor : Editor
	{
		private Dictionary<CirclePositionProvider, SphereBoundsHandle> _handles;

		private void OnEnable() =>
				_handles = targets.ToDictionary(
						t => t as CirclePositionProvider,
						t =>
						{
							var provider = t as CirclePositionProvider;

							return new SphereBoundsHandle
							{
									center = provider.Center,
									radius = provider.Radius,
							};
						});

		private void OnSceneGUI()
		{
			foreach((CirclePositionProvider provider, SphereBoundsHandle handle) in _handles)
				Draw(provider, handle);
		}

		private static void Draw(CirclePositionProvider provider, SphereBoundsHandle handle)
		{
			Axis axis = provider.NormalAxis;

			Handles.color = axis.ToColor();

			Vector3 position = provider.transform.position;

			handle.center = position + provider.Center;
			handle.radius = provider.Radius;

			handle.axes = axis.ToAxes();

			handle.DrawHandle();

			provider.Center = provider.transform.InverseTransformPoint(handle.center);
			provider.Radius = handle.radius;
		}
	}
}
