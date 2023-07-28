using System;
using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEditor.IMGUI.Controls;

using UnityEngine;

namespace RandomPlacer.PositionProviders
{
	[CustomEditor(typeof(BoxPositionProvider))]
	[CanEditMultipleObjects]
	public class BoxPositionProviderEditor : Editor
	{
		private BoxPositionProvider _boxPositionProvider;
		private BoxBoundsHandle _handle;
		private Dictionary<BoxPositionProvider, BoxBoundsHandle> _handles;

		private void OnEnable() =>
				_handles = targets.ToDictionary(
						t => t as BoxPositionProvider,
						t =>
						{
							var provider = t as BoxPositionProvider;

							return new BoxBoundsHandle
							{
									center = provider.Center,
									size = provider.Size,
							};
						});

		private void OnSceneGUI()
		{
			foreach((BoxPositionProvider provider, BoxBoundsHandle handle) in _handles)
				Draw(provider, handle);
		}

		private static void Draw(BoxPositionProvider provider, BoxBoundsHandle handle)
		{
			Vector3 position = provider.transform.position;
			Vector3 center = provider.Center;

			handle.center = position + center;
			handle.size = provider.Size;

			handle.DrawHandle();

			provider.Center = provider.transform.InverseTransformPoint(handle.center);
			provider.Size = handle.size;
		}
	}
}
