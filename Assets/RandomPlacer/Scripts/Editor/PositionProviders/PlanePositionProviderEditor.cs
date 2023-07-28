using System.Collections.Generic;
using System.Linq;

using UnityEditor;
using UnityEditor.IMGUI.Controls;

using UnityEngine;

namespace RandomPlacer.PositionProviders
{
	using Extensions;

	[CustomEditor(typeof(PlanePositionProvider))]
	[CanEditMultipleObjects]
	public class PlanePositionProviderEditor : Editor
	{
		private Dictionary<PlanePositionProvider, BoxBoundsHandle> _handles;

		private void OnEnable() =>
				_handles = targets.ToDictionary(
						t => t as PlanePositionProvider,
						t =>
						{
							var provider = t as PlanePositionProvider;

							return new BoxBoundsHandle
							{
									center = provider.Center,
									size = provider.GetInternalSizeByAxis(),
							};
						});

		private void OnSceneGUI()
		{
			foreach((PlanePositionProvider provider, BoxBoundsHandle handle) in _handles)
				Draw(provider, handle);
		}

		private static void Draw(PlanePositionProvider provider, BoxBoundsHandle handle)
		{
			Vector3 position = provider.transform.position;
			Vector3 center = provider.Center;

			handle.center = position + center;
			handle.size = provider.GetInternalSizeByAxis();

			handle.wireframeColor = provider.NormalAxis.ToColor();
			handle.DrawHandle();

			provider.Center = provider.transform.InverseTransformPoint(handle.center);
			provider.Size = provider.ConvertSizeToAxis(handle.size);
		}
	}
}
