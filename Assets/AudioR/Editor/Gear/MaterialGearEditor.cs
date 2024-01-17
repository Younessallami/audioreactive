
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(MaterialGear)), CanEditMultipleObjects]
public class MaterialGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propMaterialIndex;
    SerializedProperty propTargetType;
    SerializedProperty propTargetName;
    SerializedProperty propThreshold;
    SerializedProperty propColorGradient;
    SerializedProperty propFloatCurve;
    SerializedProperty propVectorFrom;
    SerializedProperty propVectorTo;
    SerializedProperty propTextureLow;
    SerializedProperty propTextureHigh;

    GUIContent labelFrom;
    GUIContent labelTo;
    GUIContent labelLow;
    GUIContent labelHigh;

    void OnEnable()
    {
        propReaktor       = serializedObject.FindProperty("reaktor");
        propMaterialIndex = serializedObject.FindProperty("materialIndex");
        propTargetType    = serializedObject.FindProperty("targetType");
        propTargetName    = serializedObject.FindProperty("targetName");
        propThreshold     = serializedObject.FindProperty("threshold");
        propColorGradient = serializedObject.FindProperty("colorGradient");
        propFloatCurve    = serializedObject.FindProperty("floatCurve");
        propVectorFrom    = serializedObject.FindProperty("vectorFrom");
        propVectorTo      = serializedObject.FindProperty("vectorTo");
        propTextureLow    = serializedObject.FindProperty("textureLow");
        propTextureHigh   = serializedObject.FindProperty("textureHigh");

        labelFrom = new GUIContent("From");
        labelTo   = new GUIContent("To");
        labelLow  = new GUIContent("Low");
        labelHigh = new GUIContent("High");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propReaktor);
        EditorGUILayout.PropertyField(propMaterialIndex);
        EditorGUILayout.PropertyField(propTargetType);
        EditorGUILayout.PropertyField(propTargetName);

        if (!propTargetType.hasMultipleDifferentValues &&
            propTargetType.enumValueIndex == (int)MaterialGear.TargetType.Color)
        {
            EditorGUILayout.PropertyField(propColorGradient);
        }

        if (!propTargetType.hasMultipleDifferentValues &&
            propTargetType.enumValueIndex == (int)MaterialGear.TargetType.Float)
        {
            EditorGUILayout.PropertyField(propFloatCurve);
        }

        if (!propTargetType.hasMultipleDifferentValues &&
            propTargetType.enumValueIndex == (int)MaterialGear.TargetType.Vector)
        {
            EditorGUILayout.PropertyField(propVectorFrom, labelFrom, true);
            EditorGUILayout.PropertyField(propVectorTo, labelTo, true);
        }

        if (!propTargetType.hasMultipleDifferentValues &&
            propTargetType.enumValueIndex == (int)MaterialGear.TargetType.Texture)
        {
            EditorGUILayout.PropertyField(propThreshold);
            EditorGUILayout.PropertyField(propTextureLow, labelLow);
            EditorGUILayout.PropertyField(propTextureHigh, labelHigh);
        }

        serializedObject.ApplyModifiedProperties();
    }
}

} 
