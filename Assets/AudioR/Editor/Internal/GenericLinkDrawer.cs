
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomPropertyDrawer(typeof(GenericLinkBase), true)]
class GenericLinkDrawer : PropertyDrawer
{
    // Labels and values for the mode selector.
    static GUIContent[] modeLabels = {
        new GUIContent("Not Bound"),
        new GUIContent("Auto Bind"),
        new GUIContent("Bind By Reference"),
        new GUIContent("Bind By Name")
    };
    static int[] modeValues = { 0, 1, 2, 3 };

    int GetRowCount(SerializedProperty property)
    {
        // Fully expand if it has multiple mode values.
        var propMode = property.FindPropertyRelative("_mode");
        if (propMode.hasMultipleDifferentValues) return 3;

        // Expand when by-reference and by-name mode.
        var mode = (InjectorLink.Mode)propMode.intValue;
        if (mode == InjectorLink.Mode.ByReference ||
            mode == InjectorLink.Mode.ByName) return 2;

        // Just one line.
        return 1;
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        var rowCount = GetRowCount(property);
        return EditorGUIUtility.singleLineHeight * rowCount +
               EditorGUIUtility.standardVerticalSpacing * (rowCount - 1);
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position.height = EditorGUIUtility.singleLineHeight;

        // First line: mode selector.
        var propMode = property.FindPropertyRelative("_mode");
        EditorGUI.IntPopup(position, propMode, modeLabels, modeValues, label);
        position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

        // Indent the line.
        position.width -= 16;
        position.x += 16;
        EditorGUIUtility.labelWidth -= 16;

        // Reference box.
        var mode = (InjectorLink.Mode)propMode.intValue;
        if (propMode.hasMultipleDifferentValues || mode == InjectorLink.Mode.ByReference)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_reference"));
            position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        }

        // Name box.
        if (propMode.hasMultipleDifferentValues || mode == InjectorLink.Mode.ByName)
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_name"));

        // Update the link when it gets updated.
        if (GUI.changed) property.FindPropertyRelative("_forceUpdate").boolValue = true;

        EditorGUI.EndProperty();
    }
}

} 
