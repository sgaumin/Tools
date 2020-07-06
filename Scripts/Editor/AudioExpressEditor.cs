using UnityEditor;
using UnityEngine;

namespace Tools.Utils
{
	[CustomPropertyDrawer(typeof(AudioExpress))]
	public class AudioExpressEditor : PropertyDrawer
	{
		public bool unfold = false;

		public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
		{
			return base.GetPropertyHeight(property, label) +
				(unfold
					? EditorGUIUtility.singleLineHeight * property.enumNames.Length
					: 0f);
		}

		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			// References
			SerializedProperty isUsingClips = property.FindPropertyRelative("isUsingClips");
			SerializedProperty clip = property.FindPropertyRelative("clip");
			SerializedProperty clips = property.FindPropertyRelative("clips");
			SerializedProperty mixerGroup = property.FindPropertyRelative("mixerGroup");

			// Audio Parameters
			SerializedProperty loopType = property.FindPropertyRelative("loopType");
			SerializedProperty timeBetweenLoop = property.FindPropertyRelative("timeBetweenLoop");
			SerializedProperty isPitchModified = property.FindPropertyRelative("isPitchModified");
			SerializedProperty pitchMaxVariation = property.FindPropertyRelative("pitchMaxVariation");

			// Component Behavior
			SerializedProperty autoDestroy = property.FindPropertyRelative("autoDestroy");
			SerializedProperty multiplier = property.FindPropertyRelative("multiplier");

			EditorGUI.BeginProperty(position, label, property);
			property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);

			if (property.isExpanded)
			{
				// Display
				property.serializedObject.Update();

				EditorGUI.indentLevel += 1;

				EditorGUILayout.PropertyField(isUsingClips);
				if (isUsingClips.boolValue)
				{
					EditorGUILayout.PropertyField(clips, true);
				}
				else
				{
					EditorGUILayout.PropertyField(clip);
				}
				EditorGUILayout.PropertyField(mixerGroup);

				EditorGUILayout.PropertyField(loopType);
				if (loopType.enumValueIndex == (int)AudioLoopType.Manuel)
				{
					EditorGUILayout.PropertyField(timeBetweenLoop);
				}
				EditorGUILayout.PropertyField(isPitchModified, new GUIContent("Modify Pitch"));
				if (isPitchModified.boolValue)
				{
					EditorGUILayout.PropertyField(pitchMaxVariation);
				}

				EditorGUILayout.PropertyField(autoDestroy);
				if (autoDestroy.enumValueIndex != (int)AudioStopType.No)
				{
					EditorGUILayout.PropertyField(
					  multiplier, new GUIContent(autoDestroy.enumValueIndex == (int)AudioStopType.StopAfterDuration ?
					  "Seconds" : "Play Count"));
				}

				EditorGUI.indentLevel -= 1;

			}

			EditorGUI.EndProperty();
		}
	}
}
