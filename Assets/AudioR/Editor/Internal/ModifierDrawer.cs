
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomPropertyDrawer(typeof(Modifier))]
class ModifierDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = EditorGUIUtility.singleLineHeight;

        // Expand (double the height and add space) if enabled.
        var propEnabled = property.FindPropertyRelative("enabled");
        if (propEnabled.hasMultipleDifferentValues || propEnabled.boolValue)
            height = height * 2 + EditorGUIUtility.standardVerticalSpacing;

        return height;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var propEnabled = property.FindPropertyRelative("enabled");
        var expand = propEnabled.hasMultipleDifferentValues || propEnabled.boolValue;

        // First line: enabler switch.
        position.height = EditorGUIUtility.singleLineHeight;
        EditorGUI.PropertyField(position, propEnabled, label);

        if (expand)
        {
            // Go to the next line.
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // Indent the line.
            position.width -= 16;
            position.x += 16;

            // Shorten the labels.
            EditorGUIUtility.labelWidth = 26;

            // Split into the three areas.
            position.width = (position.width - 8) / 3;

            // "Min"
            EditorGUI.PropertyField(position, property.FindPropertyRelative("min"), new GUIContent("Min"));

            // "Max"
            position.x += position.width + 4;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("max"), new GUIContent("Max"));

            // Curve (no label)
            position.x += position.width + 4;
            EditorGUI.PropertyField(position, property.FindPropertyRelative("curve"), GUIContent.none);
        }

        EditorGUI.EndProperty();
    }
}

} 