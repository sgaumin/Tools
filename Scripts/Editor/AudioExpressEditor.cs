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
      return 8;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      // References
      SerializedProperty isUsingClips = property.FindPropertyRelative("isUsingClips");
      SerializedProperty clip = property.FindPropertyRelative("clip");
      SerializedProperty clips = property.FindPropertyRelative("clips");
      SerializedProperty mixerGroup = property.FindPropertyRelative("mixerGroup");

      // Audio Parameters
      SerializedProperty attached = property.FindPropertyRelative("attached"); 
      SerializedProperty loop = property.FindPropertyRelative("loop"); 
      SerializedProperty isPitchModified = property.FindPropertyRelative("isPitchModified"); 
      SerializedProperty pitchMaxVariation = property.FindPropertyRelative("pitchMaxVariation");

      // Component Behavior
      SerializedProperty isDontDestroyOnLoad = property.FindPropertyRelative("isDontDestroyOnLoad");
      SerializedProperty autoDestroy = property.FindPropertyRelative("autoDestroy");
      SerializedProperty multiplier = property.FindPropertyRelative("multiplier");

      unfold = EditorGUI.Foldout(position, unfold, label);

      if (unfold)
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

        EditorGUILayout.PropertyField(attached);
        EditorGUILayout.PropertyField(loop);
        EditorGUILayout.PropertyField(isPitchModified, new GUIContent("Modify Pitch"));
        if (isPitchModified.boolValue)
        {
          EditorGUILayout.PropertyField(pitchMaxVariation);
        }

        EditorGUILayout.PropertyField(isDontDestroyOnLoad);
        EditorGUILayout.PropertyField(autoDestroy);
        if (autoDestroy.enumValueIndex != (int)AudioExpress.AutoDestroyTypes.No)
        {
          EditorGUILayout.PropertyField(
            multiplier, new GUIContent(autoDestroy.enumValueIndex == (int)AudioExpress.AutoDestroyTypes.AutoDestroyAfterDuration ?
            "Seconds" : "Play Count"));
        }

        EditorGUI.indentLevel -= 1;

        property.serializedObject.ApplyModifiedProperties();
      }
    }
  }
}
