
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(SpawnerGear)), CanEditMultipleObjects]
public class SpawnerGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propBurst;
    SerializedProperty propBurstNumber;
    SerializedProperty propSpawnRate;
    GUIContent labelBurstNumber;

    void OnEnable()
    {
        propReaktor      = serializedObject.FindProperty("reaktor");
        propBurst        = serializedObject.FindProperty("burst");
        propBurstNumber  = serializedObject.FindProperty("burstNumber");
        propSpawnRate    = serializedObject.FindProperty("spawnRate");
        labelBurstNumber = new GUIContent("Spawn Count");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();

        EditorGUILayout.PropertyField(propReaktor);

        EditorGUILayout.PropertyField(propBurst);
        if (propBurst.hasMultipleDifferentValues ||
            propBurst.FindPropertyRelative("enabled").boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(propBurstNumber, labelBurstNumber);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(propSpawnRate);

        serializedObject.ApplyModifiedProperties();
    }
}

} 
