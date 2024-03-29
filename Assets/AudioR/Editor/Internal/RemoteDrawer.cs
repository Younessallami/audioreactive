
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomPropertyDrawer(typeof(Remote))]
class RemoteDrawer : PropertyDrawer
{
    // Labels and values for the control types.
    static GUIContent[] controlLabels = {
        new GUIContent("Off"),
        new GUIContent("MIDI CC"),
        new GUIContent("MIDI Note"),
        new GUIContent("Input Axis")
    };
    static int[] controlValues = { 0, 1, 2, 3 };

    int GetRowCount(SerializedProperty property)
    {
        // Fully expand if it has multiple mode values.
        var propControl = property.FindPropertyRelative("_control");
        if (propControl.hasMultipleDifferentValues) return 6;

        // Uses four rows when it has MIDI options.
        var control = (Remote.Control)propControl.intValue;
        if (control == Remote.Control.MidiKnob ||
            control == Remote.Control.MidiNote) return 4;

        // Expand if it's enabled.
        return control > 0 ? 3 : 1;
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

        // First line: Control selector.
        var propControl = property.FindPropertyRelative("_control");
        EditorGUI.IntPopup(position, propControl, controlLabels, controlValues, label);

        var nextLine = EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
        position.y += nextLine;

        // Indent the line.
        var indent = 16;
        position.width -= indent;
        position.x += indent;
        EditorGUIUtility.labelWidth -= indent;

        // MIDI channel selector.
        var control = (Remote.Control)propControl.intValue;
        if (propControl.hasMultipleDifferentValues ||
            control == Remote.Control.MidiKnob || control == Remote.Control.MidiNote)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_midiChannel"), new GUIContent("MIDI Channel"));
            position.y += nextLine;
        }

        // MIDI CC index box.
        if (propControl.hasMultipleDifferentValues || control == Remote.Control.MidiKnob)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_knobIndex"), new GUIContent("CC Number"));
            position.y += nextLine;
        }

        // MIDI note number box.
        if (propControl.hasMultipleDifferentValues || control == Remote.Control.MidiNote)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_noteNumber"));
            position.y += nextLine;
        }

        // Input axis name box.
        if (propControl.hasMultipleDifferentValues || control == Remote.Control.InputAxis)
        {
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_inputAxis"));
            position.y += nextLine;
        }

        // Curve editor.
        if (propControl.hasMultipleDifferentValues || control != Remote.Control.Off)
            EditorGUI.PropertyField(position, property.FindPropertyRelative("_curve"));

        EditorGUI.EndProperty();
    }
}

} 
