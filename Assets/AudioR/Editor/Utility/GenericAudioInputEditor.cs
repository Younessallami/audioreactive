
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(GenericAudioInput))]
public class GenericAudioInputEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlaying)
        {
            var latency = (target as GenericAudioInput).estimatedLatency * 1000;
            EditorGUILayout.HelpBox ("Estimated latency = " + latency + " ms", MessageType.None);
        }
    }
}

} 
