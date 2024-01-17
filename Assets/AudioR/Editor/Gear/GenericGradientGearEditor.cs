
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(GenericGradientGear)), CanEditMultipleObjects]
public class GenericGradientGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propGradient;
    SerializedProperty propTarget;

    void OnEnable()
    {
        propReaktor  = serializedObject.FindProperty("reaktor");
        propGradient = serializedObject.FindProperty("gradient");
        propTarget   = serializedObject.FindProperty("target");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();

        EditorGUILayout.PropertyField(propReaktor);
        EditorGUILayout.PropertyField(propGradient);
        EditorGUILayout.PropertyField(propTarget);

        serializedObject.ApplyModifiedProperties ();
    }
}

} 
