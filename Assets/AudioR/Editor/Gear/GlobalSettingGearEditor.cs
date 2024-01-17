
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(GlobalSettingGear)), CanEditMultipleObjects]
public class GlobalSettingGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propTimeScaleCurve;

    void OnEnable()
    {
        propReaktor = serializedObject.FindProperty("reaktor");
        propTimeScaleCurve = serializedObject.FindProperty("timeScaleCurve");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();

        EditorGUILayout.PropertyField(propReaktor);
        EditorGUILayout.PropertyField(propTimeScaleCurve);

        serializedObject.ApplyModifiedProperties ();
    }
}

} 
