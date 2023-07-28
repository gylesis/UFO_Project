using UnityEditor;

using UnityEngine;

namespace RandomPlacer.ScaleProviders
{
	[CustomPropertyDrawer(typeof(ScaleConstraint))]
	public class ScaleConstraintPropertyDrawer : PropertyDrawer
	{
		private float _minLimit = 1;
		private float _maxLimit = 10;

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			DrawMinMaxSliderWithFloatFields(property);
		}

		private void DrawMinMaxSliderWithFloatFields(SerializedProperty property)
		{
			SerializedProperty min = property.FindPropertyRelative("Min");
			SerializedProperty max = property.FindPropertyRelative("Max");

			float minFloatValue = min.floatValue;
			float maxFloatValue = max.floatValue;

			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(property.displayName);
			EditorGUILayout.EndHorizontal();

			_minLimit = EditorGUILayout.FloatField("Min Limit", _minLimit);
			_maxLimit = EditorGUILayout.FloatField("Max Limit", _maxLimit);

			EditorGUILayout.BeginHorizontal();

			minFloatValue = EditorGUILayout.FloatField(minFloatValue, GUILayout.MaxWidth(50));

			EditorGUILayout.MinMaxSlider(ref minFloatValue, ref maxFloatValue, _minLimit, _maxLimit);

			maxFloatValue = EditorGUILayout.FloatField(maxFloatValue, GUILayout.MaxWidth(50));
			EditorGUILayout.EndHorizontal();

			min.floatValue = minFloatValue;
			max.floatValue = maxFloatValue;
		}
	}
}
