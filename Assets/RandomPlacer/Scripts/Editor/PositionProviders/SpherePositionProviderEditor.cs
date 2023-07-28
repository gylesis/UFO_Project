using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEditor.IMGUI.Controls;

using UnityEngine;

namespace RandomPlacer.PositionProviders
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(SpherePositionProvider))]
	public class SpherePositionProviderEditor : Editor
	{
		private SpherePositionProvider _spherePositionProvider;
		private Dictionary<SpherePositionProvider, SphereBoundsHandle> _handles;

		private void OnEnable() =>
				_handles = targets.ToDictionary(
						t => t as SpherePositionProvider,
						t =>
						{
							var provider = t as SpherePositionProvider;

							return new SphereBoundsHandle
							{
									center = provider.Center,
									radius = provider.Radius,
							};
						});

		private void OnSceneGUI()
		{
			foreach((SpherePositionProvider provider, SphereBoundsHandle handle) in _handles)
				Draw(provider, handle);
		}

		private static void Draw(SpherePositionProvider provider, SphereBoundsHandle handle)
		{
			Vector3 position = provider.transform.position;

			handle.center = position + provider.Center;
			handle.radius = provider.Radius;

			handle.DrawHandle();

			provider.Center = provider.transform.InverseTransformPoint(handle.center);
			provider.Radius = handle.radius;
		}
	}
}
