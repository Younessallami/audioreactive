
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(TurbulentMotion)), CanEditMultipleObjects]
public class TurbulentMotionEditor : Editor
{
    SerializedProperty propDensity;
    SerializedProperty propLinearFlow;

    SerializedProperty propDisplacement;
    SerializedProperty propRotation;
    SerializedProperty propScale;
    SerializedProperty propCoeffDisplacement;
    SerializedProperty propCoeffRotation;
    SerializedProperty propCoeffScale;

    SerializedProperty propUseLocalCoordinate;

    GUIContent labelAmplitude;
    GUIContent labelWaveNumber;
    GUIContent labelInfluence;
    GUIContent labelLocalCoordinate;
    
    void OnEnable()
    {
        propDensity    = serializedObject.FindProperty("density");
        propLinearFlow = serializedObject.FindProperty("linearFlow");

        propDisplacement      = serializedObject.FindProperty("displacement");
        propRotation          = serializedObject.FindProperty("rotation");
        propScale             = serializedObject.FindProperty("scale");
        propCoeffDisplacement = serializedObject.FindProperty("coeffDisplacement");
        propCoeffRotation     = serializedObject.FindProperty("coeffRotation");
        propCoeffScale        = serializedObject.FindProperty("coeffScale");

        propUseLocalCoordinate = serializedObject.FindProperty("useLocalCoordinate");

        labelAmplitude       = new GUIContent("Amplitude");
        labelWaveNumber      = new GUIContent("Wave Number");
        labelInfluence       = new GUIContent("Influence (≦1.0)");
        labelLocalCoordinate = new GUIContent("Local Coordinate");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Noise");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propDensity);
        EditorGUILayout.PropertyField(propLinearFlow);
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Displacement");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propDisplacement, labelAmplitude);
        EditorGUILayout.PropertyField(propCoeffDisplacement, labelWaveNumber);
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Rotation (Euler)");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propRotation, labelAmplitude);
        EditorGUILayout.PropertyField(propCoeffRotation, labelWaveNumber);
        EditorGUI.indentLevel--;

        EditorGUILayout.LabelField("Scale");
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propScale, labelInfluence);
        EditorGUILayout.PropertyField(propCoeffScale, labelWaveNumber);
        EditorGUI.indentLevel--;

        EditorGUILayout.PropertyField(propUseLocalCoordinate, labelLocalCoordinate);

        serializedObject.ApplyModifiedProperties();
    }
}

} 
