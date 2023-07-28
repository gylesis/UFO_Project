using UnityEditor;

using UnityEngine;

namespace RandomPlacer.RotationProviders
{
	[CustomPropertyDrawer(typeof(AngleConstraint))]
	public class RotationProvidersPropertyDrawer : PropertyDrawer
	{
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

			EditorGUILayout.BeginHorizontal();

			minFloatValue = EditorGUILayout.FloatField(minFloatValue, GUILayout.MaxWidth(50));

			EditorGUILayout.MinMaxSlider(ref minFloatValue, ref maxFloatValue, 0, 360);

			maxFloatValue = EditorGUILayout.FloatField(maxFloatValue, GUILayout.MaxWidth(50));
			EditorGUILayout.EndHorizontal();

			if (minFloatValue > maxFloatValue)
				minFloatValue = maxFloatValue;

			if (maxFloatValue < minFloatValue)
				maxFloatValue = minFloatValue;

			if (minFloatValue < 0)
				minFloatValue = 0;

			if (maxFloatValue > 360)
				maxFloatValue = 360;

			min.floatValue = minFloatValue;
			max.floatValue = maxFloatValue;
		}

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label);
		}
	}
}
