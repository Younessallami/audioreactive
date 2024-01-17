
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(JitterMotionGear)), CanEditMultipleObjects]
public class JitterMotionGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propPositionFrequency;
    SerializedProperty propRotationFrequency;
    SerializedProperty propPositionAmount;
    SerializedProperty propRotationAmount;
    GUIContent labelPositionAmount;
    GUIContent labelRotationAmount;
    GUIContent labelFrequency;

    void OnEnable()
    {
        propReaktor  = serializedObject.FindProperty("reaktor");
        propPositionFrequency = serializedObject.FindProperty("positionFrequency");
        propRotationFrequency = serializedObject.FindProperty("rotationFrequency");
        propPositionAmount = serializedObject.FindProperty("positionAmount");
        propRotationAmount = serializedObject.FindProperty("rotationAmount");
        labelPositionAmount = new GUIContent("Position Noise");
        labelRotationAmount = new GUIContent("Rotation Noise");
        labelFrequency = new GUIContent("Frequency");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propReaktor);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(propPositionAmount, labelPositionAmount);
        EditorGUILayout.PropertyField(propPositionFrequency, labelFrequency);

        EditorGUILayout.Space();

        EditorGUILayout.PropertyField(propRotationAmount, labelRotationAmount);
        EditorGUILayout.PropertyField(propRotationFrequency, labelFrequency);

        serializedObject.ApplyModifiedProperties();
    }
}

} 
