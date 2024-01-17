
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(ObjectParticleSystem)), CanEditMultipleObjects]
public class ObjectParticleSystemEditor : Editor
{
    SerializedProperty propPrefab;
    SerializedProperty propMaxParticles;

    void OnEnable()
    {
        propPrefab = serializedObject.FindProperty("prefab");
        propMaxParticles = serializedObject.FindProperty("maxParticles");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(propPrefab);
        EditorGUILayout.PropertyField(propMaxParticles);
        serializedObject.ApplyModifiedProperties();
    }
}

} 
