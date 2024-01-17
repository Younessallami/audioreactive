
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(ParticleSystemGear)), CanEditMultipleObjects]
public class ParticleSystemGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propBurst;
    SerializedProperty propBurstNumber;
    SerializedProperty propEmissionRate;
    SerializedProperty propSize;
    GUIContent labelBurstNumber;

    void OnEnable()
    {
        propReaktor      = serializedObject.FindProperty("reaktor");
        propBurst        = serializedObject.FindProperty("burst");
        propBurstNumber  = serializedObject.FindProperty("burstNumber");
        propEmissionRate = serializedObject.FindProperty("emissionRate");
        propSize         = serializedObject.FindProperty("size");
        labelBurstNumber = new GUIContent("Particles");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propReaktor);

        EditorGUILayout.PropertyField(propBurst);
        if (propBurst.hasMultipleDifferentValues ||
            propBurst.FindPropertyRelative("enabled").boolValue)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(propBurstNumber, labelBurstNumber);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(propEmissionRate);

        EditorGUILayout.PropertyField(propSize);

        serializedObject.ApplyModifiedProperties();
    }
}

} 
