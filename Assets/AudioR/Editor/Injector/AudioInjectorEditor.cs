
using UnityEngine;
using UnityEditor;
using System.Collections;

namespace Reaktion {

[CustomEditor(typeof(AudioInjector)), CanEditMultipleObjects]
public class AudioInjectorEditor : Editor
{
    SerializedProperty propMute;

    // Assets for drawing level meters.
    Texture2D bgTexture;
    Texture2D fgTexture;

    void OnEnable()
    {
        propMute = serializedObject.FindProperty("mute");
    }

    void OnDisable()
    {
        if (bgTexture != null)
        {
            DestroyImmediate(bgTexture);
            DestroyImmediate(fgTexture);
            bgTexture = fgTexture = null;
        }
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(propMute);

        serializedObject.ApplyModifiedProperties();

        // Draw a level meter if the target is active.
        if (EditorApplication.isPlaying && !serializedObject.isEditingMultipleObjects)
        {
            var source = target as AudioInjector;
            if (source.enabled && source.gameObject.activeInHierarchy)
            {
                DrawLevelMeter(source.DbLevel);
                EditorUtility.SetDirty(target); // Make it dirty to update the view.
            }
        }
    }

    static Texture2D NewBarTexture(Color color)
    {
        var texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        return texture;
    }

    void DrawLevelMeter(float level)
    {
        if (bgTexture == null)
        {
            bgTexture = NewBarTexture(new Color(55.0f / 255, 53.0f / 255, 45.0f / 255));    // gray
            fgTexture = NewBarTexture(new Color(250.0f / 255, 249.0f / 255, 248.0f / 255)); // white
        }

        // Draw BG.
        var rect = GUILayoutUtility.GetRect(18, 16, "TextField");
        GUI.DrawTexture(rect, bgTexture);

        // Draw level bar.
        var barRect = rect;
        barRect.width *= Mathf.Clamp01((level + 60) / (3 + 60));
        GUI.DrawTexture(barRect, fgTexture);

        // Draw dB value.
        EditorGUI.LabelField(rect, level.ToString("0.0") + " dB");
    }
}

} 
