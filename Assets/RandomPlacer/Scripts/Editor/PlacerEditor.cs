using System.Collections.Generic;
using System.Linq;

using RandomPlacer.ScaleProviders;

using UnityEditor;

using UnityEngine;

using Object = UnityEngine.Object;

namespace RandomPlacer
{
	[CustomEditor(typeof(Placer))]
	[CanEditMultipleObjects]
	public class PlacerEditor : Editor
	{
		private BoundsType _lastBoundsType;
		private RotationType _rotationType;
		private ScaleType _scaleType;

		private PositionProvider _currentPositionProvider;
		private RotationProvider _currentRotationProvider;
		private ScaleProvider _currentScaleProvider;
		private Dictionary<Object, PlacerEditorContext> _contexts;

		private void OnEnable()
		{
			serializedObject.Update();

			_contexts = targets.ToDictionary(t => t, t => new PlacerEditorContext(t as Placer));
			
			foreach(Object target in targets)
				_contexts[target].Validate();

			serializedObject.ApplyModifiedProperties();
		}

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();
			serializedObject.Update();

			Validate();

			DrawButtons();

			serializedObject.ApplyModifiedProperties();
		}

		private void Validate()
		{
			foreach(Object target in targets)
				_contexts[target].Validate();
		}

		private void DrawButtons()
		{
			EditorGUILayout.BeginHorizontal();

			if (GUILayout.Button("Place"))
				foreach(Object t in targets)
					_contexts[t].Place();

			if (GUILayout.Button("Clear"))
				foreach(Object t in targets)
					_contexts[t].Clear();

			if (GUILayout.Button("Replace"))
				foreach(Object t in targets)
					_contexts[t].Replace();

			EditorGUILayout.EndHorizontal();
		}
	}
}
