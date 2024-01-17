
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(GenericTriggerGear)), CanEditMultipleObjects]
public class GenericTriggerGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propThreshold;
    SerializedProperty propInterval;
    SerializedProperty propTarget;
    GUIContent labelInterval;

    void OnEnable()
    {
        propReaktor   = serializedObject.FindProperty("reaktor");
        propThreshold = serializedObject.FindProperty("threshold");
        propInterval  = serializedObject.FindProperty("interval");
        propTarget    = serializedObject.FindProperty("target");
        labelInterval = new GUIContent("Minimum Interval");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();

        EditorGUILayout.PropertyField(propReaktor);

        EditorGUILayout.Slider(propThreshold, 0.01f, 0.99f);
        EditorGUILayout.PropertyField(propInterval, labelInterval);

        EditorGUILayout.PropertyField(propTarget);

        serializedObject.ApplyModifiedProperties ();
    }
}

} 
