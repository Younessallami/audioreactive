﻿
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

// Custom property drawer for TransformElement.
[CustomPropertyDrawer(typeof(TransformGear.TransformElement))]
class TransformGearElementDrawer : PropertyDrawer
{
    // Labels and values for TransformMode.
    static GUIContent[] modeLabels = {
        new GUIContent("Off"),
        new GUIContent("X Axis"),
        new GUIContent("Y Axis"),
        new GUIContent("Z Axis"),
        new GUIContent("Arbitrary Vector"),
        new GUIContent("Random Vector")
    };
    static int[] modeValues = { 0, 1, 2, 3, 4, 5 };

    static int GetExpansionLevel(SerializedProperty property)
    {
        var mode = property.FindPropertyRelative("mode");
        // Fully expand if it has different values.
        if (mode.hasMultipleDifferentValues) return 2;
        // "Off"
        if (mode.enumValueIndex == 0) return 0;
        // Fully expand if it's in Arbitrary mode.
        if (mode.enumValueIndex == (int)TransformGear.TransformMode.Arbitrary) return 2;
        // Expand one level.
        return 1;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        int rows = new int[]{1, 3, 4}[GetExpansionLevel(property)];
        return EditorGUIUtility.singleLineHeight * rows +
               EditorGUIUtility.standardVerticalSpacing * (rows - 1);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position.height = EditorGUIUtility.singleLineHeight;
        var rowHeight = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        // Transform mode selector drop-down.
        EditorGUI.IntPopup(position, property.FindPropertyRelative("mode"), modeLabels, modeValues, label);
        position.y += rowHeight;

        var expansion = GetExpansionLevel(property);
        if (expansion > 0)
        {
            // Insert an indent.
            position.x += 16;
            position.width -= 16;
            EditorGUIUtility.labelWidth -= 16;

            if (expansion == 2)
            {
                // Vector box.
                EditorGUI.PropertyField(position, property.FindPropertyRelative("arbitraryVector"), GUIContent.none);
                position.y += rowHeight;
            }

            // Shorten the labels.
            EditorGUIUtility.labelWidth = 26;

            // Split into three columns.
            var column = position;
            column.width = (column.width - 8) / 3;
            position.y += rowHeight;

            // Min value box.
            EditorGUI.PropertyField(column, property.FindPropertyRelative("min"));
            column.x += column.width + 4;

            // Max value box.
            EditorGUI.PropertyField(column, property.FindPropertyRelative("max"));
            column.x += column.width + 4;

            // Curve (no label).
            EditorGUI.PropertyField(column, property.FindPropertyRelative("curve"), GUIContent.none);

            // Re-expand the labels.
            EditorGUIUtility.labelWidth = 0;
            EditorGUIUtility.labelWidth -= 16;

            // Randomness slider.
            EditorGUI.Slider(position, property.FindPropertyRelative("randomness"), 0, 1);
        }

        EditorGUI.EndProperty();
    }
}

[CustomEditor(typeof(TransformGear)), CanEditMultipleObjects]
public class TransformGearEditor : Editor
{
    SerializedProperty propReaktor;

    SerializedProperty propPosition;
    SerializedProperty propRotation;
    SerializedProperty propScale;

    SerializedProperty propAddInitial;

    void OnEnable()
    {
        propReaktor  = serializedObject.FindProperty("reaktor");

        propPosition = serializedObject.FindProperty("position");
        propRotation = serializedObject.FindProperty("rotation");
        propScale    = serializedObject.FindProperty("scale");

        propAddInitial = serializedObject.FindProperty("addInitialValue");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propReaktor);

        EditorGUILayout.PropertyField(propPosition);

        EditorGUILayout.PropertyField(propRotation);

        EditorGUILayout.PropertyField(propScale);

        EditorGUILayout.PropertyField(propAddInitial);

        serializedObject.ApplyModifiedProperties();
    }
}

} 