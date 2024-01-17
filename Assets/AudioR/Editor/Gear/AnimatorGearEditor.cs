
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(AnimatorGear)), CanEditMultipleObjects]
public class AnimatorGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propSpeed;
    SerializedProperty propTrigger;
    SerializedProperty propTriggerName;

    void OnEnable()
    {
        propReaktor     = serializedObject.FindProperty("reaktor");
        propSpeed       = serializedObject.FindProperty("speed");
        propTrigger     = serializedObject.FindProperty("trigger");
        propTriggerName = serializedObject.FindProperty("triggerName");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(propReaktor);

        EditorGUILayout.PropertyField(propSpeed);

        EditorGUILayout.PropertyField(propTrigger);

        if (propTrigger.hasMultipleDifferentValues ||
            propTrigger.FindPropertyRelative("enabled").boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(propTriggerName);
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

} 
