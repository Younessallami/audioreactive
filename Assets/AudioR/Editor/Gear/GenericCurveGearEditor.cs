
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(GenericCurveGear)), CanEditMultipleObjects]
public class GenericCurveGearEditor : Editor
{
    SerializedProperty propReaktor;
    SerializedProperty propOptionType;
    SerializedProperty propCurve;
    SerializedProperty propOrigin;
    SerializedProperty propDirection;

    SerializedProperty propBoolTarget;
    SerializedProperty propIntTarget;
    SerializedProperty propFloatTarget;
    SerializedProperty propVectorTarget;

    GUIContent labelTarget;

    void OnEnable()
    {
        propReaktor      = serializedObject.FindProperty("reaktor");
        propOptionType   = serializedObject.FindProperty("optionType");
        propCurve        = serializedObject.FindProperty("curve");
        propOrigin       = serializedObject.FindProperty("origin");
        propDirection    = serializedObject.FindProperty("direction");

        propBoolTarget   = serializedObject.FindProperty("boolTarget");
        propIntTarget    = serializedObject.FindProperty("intTarget");
        propFloatTarget  = serializedObject.FindProperty("floatTarget");
        propVectorTarget = serializedObject.FindProperty("vectorTarget");

        labelTarget = new GUIContent("Target");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update ();

        EditorGUILayout.PropertyField(propReaktor);
        EditorGUILayout.PropertyField(propOptionType);
        EditorGUILayout.PropertyField(propCurve);

        if (propOptionType.hasMultipleDifferentValues ||
            propOptionType.intValue == (int)GenericCurveGear.OptionType.Bool)
        {
            EditorGUILayout.PropertyField(propBoolTarget, labelTarget);
        }

        if (propOptionType.hasMultipleDifferentValues ||
            propOptionType.intValue == (int)GenericCurveGear.OptionType.Int)
        {
            EditorGUILayout.PropertyField(propIntTarget, labelTarget);
        }

        if (propOptionType.hasMultipleDifferentValues ||
            propOptionType.intValue == (int)GenericCurveGear.OptionType.Float)
        {
            EditorGUILayout.PropertyField(propFloatTarget, labelTarget);
        }

        if (propOptionType.hasMultipleDifferentValues ||
            propOptionType.intValue == (int)GenericCurveGear.OptionType.Vector)
        {
            EditorGUILayout.PropertyField(propOrigin);
            EditorGUILayout.PropertyField(propDirection);
            EditorGUILayout.PropertyField(propVectorTarget, labelTarget);
        }

        serializedObject.ApplyModifiedProperties ();
    }
}

} 
