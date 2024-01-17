
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomPropertyDrawer(typeof(Trigger))]
class TriggerDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var height = EditorGUIUtility.singleLineHeight;

        // Expand if enabled.
        var propEnabled = property.FindPropertyRelative("enabled");
        if (propEnabled.hasMultipleDifferentValues || propEnabled.boolValue)
            height = height * 3 + EditorGUIUtility.standardVerticalSpacing * 2;

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
            EditorGUIUtility.labelWidth -= 16;

            // "Threshold"
            EditorGUI.Slider(position, property.FindPropertyRelative("threshold"), 0.01f, 0.99f, new GUIContent("Threshold"));
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            // "Minimum Interval"
            EditorGUI.PropertyField(position, property.FindPropertyRelative("interval"), new GUIContent("Minimum Interval"));
        }

        EditorGUI.EndProperty();
    }
}

} 
