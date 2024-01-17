
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(Spawner)), CanEditMultipleObjects]
public class SpawnerEditor : Editor
{
    SerializedProperty propPrefabs;
    SerializedProperty propSpawnRate;
    SerializedProperty propSpawnRateRandomness;
    SerializedProperty propDistribution;
    SerializedProperty propSphereRadius;
    SerializedProperty propBoxSize;
    SerializedProperty propSpawnPoints;
    SerializedProperty propRandomRotation;
    SerializedProperty propParent;

    static GUIContent[] distributionLabels = {
        new GUIContent("In Sphere"),
        new GUIContent("In Box"),
        new GUIContent("At Points")
    };

    static int[] distributionValues = { 0, 1, 2 };

    void OnEnable()
    {
        propPrefabs             = serializedObject.FindProperty("prefabs");
        propSpawnRate           = serializedObject.FindProperty("spawnRate");
        propSpawnRateRandomness = serializedObject.FindProperty("spawnRateRandomness");
        propDistribution        = serializedObject.FindProperty("distribution");
        propSphereRadius        = serializedObject.FindProperty("sphereRadius");
        propBoxSize             = serializedObject.FindProperty("boxSize");
        propSpawnPoints         = serializedObject.FindProperty("spawnPoints");
        propRandomRotation      = serializedObject.FindProperty("randomRotation");
        propParent              = serializedObject.FindProperty("parent");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propPrefabs, true);

        EditorGUILayout.PropertyField(propSpawnRate);
        EditorGUILayout.Slider(propSpawnRateRandomness, 0, 1, "Randomness");

        EditorGUILayout.IntPopup(propDistribution, distributionLabels, distributionValues);

        EditorGUI.indentLevel++;

        Spawner.Distribution dist = (Spawner.Distribution)propDistribution.enumValueIndex;

        if (propDistribution.hasMultipleDifferentValues || dist == Spawner.Distribution.InSphere)
            EditorGUILayout.PropertyField(propSphereRadius);

        if (propDistribution.hasMultipleDifferentValues || dist == Spawner.Distribution.InBox)
            EditorGUILayout.PropertyField(propBoxSize);

        if (propDistribution.hasMultipleDifferentValues || dist == Spawner.Distribution.AtPoints)
            EditorGUILayout.PropertyField(propSpawnPoints, true);

        EditorGUI.indentLevel--;

        EditorGUILayout.PropertyField(propRandomRotation);
        EditorGUILayout.PropertyField(propParent, new GUIContent("Set Parent"));

        serializedObject.ApplyModifiedProperties();
    }
}

} 
