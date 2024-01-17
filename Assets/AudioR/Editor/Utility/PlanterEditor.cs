
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(Planter)), CanEditMultipleObjects]
public class PlanterEditor : Editor
{
    SerializedProperty propPrefabs;
    SerializedProperty propMaxObjects;
    SerializedProperty propDistributionMode;
    SerializedProperty propDistributionRange;
    SerializedProperty propGridSpace;
    SerializedProperty propRotationMode;
    SerializedProperty propIntervalMode;
    SerializedProperty propInterval;

    void OnEnable()
    {
        propPrefabs           = serializedObject.FindProperty("prefabs");
        propMaxObjects        = serializedObject.FindProperty("_maxObjects");
        propDistributionMode  = serializedObject.FindProperty("distributionMode");
        propDistributionRange = serializedObject.FindProperty("_distributionRange");
        propGridSpace         = serializedObject.FindProperty("_gridSpace");
        propRotationMode      = serializedObject.FindProperty("rotationMode");
        propIntervalMode      = serializedObject.FindProperty("intervalMode");
        propInterval          = serializedObject.FindProperty("_interval");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propPrefabs, true);
        EditorGUILayout.PropertyField(propMaxObjects);

        EditorGUILayout.PropertyField(propDistributionMode);
        if (propDistributionMode.hasMultipleDifferentValues ||
            propDistributionMode.enumValueIndex != (int)Planter.DistributionMode.Single)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(propDistributionRange, GUIContent.none);
            EditorGUI.indentLevel--;
        }

        if (propDistributionMode.hasMultipleDifferentValues ||
            propDistributionMode.enumValueIndex == (int)Planter.DistributionMode.Grid)
        {
            EditorGUI.indentLevel++;
            EditorGUILayout.PropertyField(propGridSpace);
            EditorGUI.indentLevel--;
        }

        EditorGUILayout.PropertyField(propRotationMode);

        EditorGUILayout.PropertyField(propIntervalMode);
        EditorGUI.indentLevel++;
        EditorGUILayout.PropertyField(propInterval);
        EditorGUI.indentLevel--;

        serializedObject.ApplyModifiedProperties();
    }
}

} 
