
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(LightGear)), CanEditMultipleObjects]
public class LightGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propIntensity;
    SerializedProperty propEnableColor;
    SerializedProperty propColorGradient;

    GUIContent labelColor;
    GUIContent labelGradient;

    void OnEnable()
    {
        propReaktor       = serializedObject.FindProperty("reaktor");
        propIntensity     = serializedObject.FindProperty("intensity");
        propEnableColor   = serializedObject.FindProperty("enableColor");
        propColorGradient = serializedObject.FindProperty("colorGradient");

        labelColor    = new GUIContent("Color");
        labelGradient = new GUIContent("Gradient");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propReaktor);

        EditorGUILayout.PropertyField(propIntensity);

        EditorGUILayout.PropertyField(propEnableColor, labelColor);
        if (propEnableColor.hasMultipleDifferentValues || propEnableColor.boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(propColorGradient, labelGradient);
            EditorGUI.indentLevel--;
        }

        serializedObject.ApplyModifiedProperties();
    }
}

} 
