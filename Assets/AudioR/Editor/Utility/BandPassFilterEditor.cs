
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(BandPassFilter)), CanEditMultipleObjects]
public class BandPassFilterEditor : Editor
{
    SerializedProperty propCutoff;
    SerializedProperty propQ;

    void OnEnable()
    {
        propCutoff = serializedObject.FindProperty("cutoff");
        propQ = serializedObject.FindProperty("q");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.Slider(propCutoff, 0.0f, 1.0f, "Cutoff");
        EditorGUILayout.Slider(propQ, 1.0f, 10.0f, "Q");

        serializedObject.ApplyModifiedProperties();
    }
}

} 
