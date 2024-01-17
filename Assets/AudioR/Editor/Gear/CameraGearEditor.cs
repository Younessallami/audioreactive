
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(CameraGear)), CanEditMultipleObjects]
public class CameraGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propFieldOfView;
    SerializedProperty propViewportWidth;
    SerializedProperty propViewportHeight;

    void OnEnable()
    {
        propReaktor        = serializedObject.FindProperty("reaktor");
        propFieldOfView    = serializedObject.FindProperty("fieldOfView");
        propViewportWidth  = serializedObject.FindProperty("viewportWidth");
        propViewportHeight = serializedObject.FindProperty("viewportHeight");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        EditorGUILayout.PropertyField(propReaktor);
        EditorGUILayout.PropertyField(propFieldOfView);
        EditorGUILayout.PropertyField(propViewportWidth);
        EditorGUILayout.PropertyField(propViewportHeight);

        serializedObject.ApplyModifiedProperties();
    }
}

} 
