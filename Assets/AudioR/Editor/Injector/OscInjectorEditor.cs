
using UnityEngine;
using UnityEditor;

namespace Reaktion
{
    [CustomEditor(typeof(OscInjector)), CanEditMultipleObjects]
    public class OscInjectorEditor : Editor
    {
        SerializedProperty propScaleMode;
        SerializedProperty propAddress;
        SerializedProperty propDataIndex;

        void OnEnable()
        {
            propScaleMode = serializedObject.FindProperty("scaleMode");
            propAddress   = serializedObject.FindProperty("address");
            propDataIndex = serializedObject.FindProperty("dataIndex");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(propScaleMode);
            EditorGUILayout.PropertyField(propAddress);
            EditorGUILayout.PropertyField(propDataIndex);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
