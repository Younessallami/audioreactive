
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(ConstantMotionGear)), CanEditMultipleObjects]
public class ConstantMotionGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propPosition;
    SerializedProperty propRotation;
    GUIContent labelPosition;
    GUIContent labelRotation;

    void OnEnable()
    {
        propReaktor  = serializedObject.FindProperty("reaktor");
        propPosition = serializedObject.FindProperty("position");
        propRotation = serializedObject.FindProperty("rotation");
        labelPosition = new GUIContent("Position (Velocity)");
        labelRotation = new GUIContent("Rotation (Velocity)");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propReaktor);
        EditorGUILayout.PropertyField(propPosition, labelPosition);
        EditorGUILayout.PropertyField(propRotation, labelRotation);

        serializedObject.ApplyModifiedProperties();
    }
}

} 
