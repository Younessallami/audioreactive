
using UnityEngine;
using UnityEditor;

namespace Reaktion {

public class ReaktionWindow : EditorWindow
{
    const int updateInterval = 15;
    int updateCounter;

    Reaktor[] cachedReaktors;
    int activeReaktorCount;

    Vector2 scrollPosition;

    [MenuItem ("Window/Reaktion")]
    static void Init ()
    {
        EditorWindow.GetWindow<ReaktionWindow> ("Reaktion");
    }

    void OnEnable ()
    {
        EditorApplication.playmodeStateChanged += OnPlaymodeStateChanged;
    }

    public void OnPlaymodeStateChanged ()
    {
        autoRepaintOnSceneChange = !EditorApplication.isPlaying;
        Repaint ();
    }

    void Update ()
    {
        if (EditorApplication.isPlaying)
        {
            if (++updateCounter >= updateInterval)
            {
                Repaint ();
                updateCounter = 0;
            }
        }
    }

    static int CompareReaktor (Reaktor a, Reaktor b)
    {
        return a.name.CompareTo(b.name);
    }

    void FindAndCacheReaktors ()
    {
        // Cache validity check.
        if (EditorApplication.isPlaying && cachedReaktors != null &&
            activeReaktorCount == Reaktor.ActiveInstanceCount)
        {
            bool validity = true;
            foreach (var r in cachedReaktors) validity &= (r!= null);
            // No update if the cache is valid.
            if (validity) return;
        }

        // Update the cache.
        cachedReaktors = FindObjectsOfType<Reaktor> ();
        System.Array.Sort (cachedReaktors, CompareReaktor);
        activeReaktorCount = Reaktor.ActiveInstanceCount;
    }

    void OnGUI ()
    {
        FindAndCacheReaktors();

        scrollPosition = EditorGUILayout.BeginScrollView (scrollPosition);

        GUILayout.Label ("Reaktor List", EditorStyles.boldLabel);

        foreach (var reaktor in cachedReaktors)
        {
            EditorGUILayout.BeginHorizontal ();

            // Slider
            if (EditorApplication.isPlaying)
            {
                if (reaktor.IsOverridden)
                {
                    // Already overridden: show the override value.
                    var value = EditorGUILayout.Slider (reaktor.name, reaktor.Override, 0, 1);
                    if (!reaktor.Bang) reaktor.Override = value;
                }
                else
                {
                    // Not overridden: show the output value and begin override when touched.
                    var value = EditorGUILayout.Slider (reaktor.name, reaktor.Output, 0, 1);
                    if (value != reaktor.Output) reaktor.Override = value;
                }
            }
            else
            {
                // Not playing: show a dummy slider.
                EditorGUILayout.Slider (reaktor.name, 0, 0, 1);
            }

            // Bang button
            if (GUILayout.RepeatButton ("!", EditorStyles.miniButtonLeft, GUILayout.Width (18)))
            {
                reaktor.Bang = true;
            }
            else if (reaktor.Bang && Event.current.type == EventType.Repaint)
            {
                reaktor.Override = 0;
            }

            // Release/Select button
            if (reaktor.IsOverridden)
            {
                if (GUILayout.Button ("Release", EditorStyles.miniButtonRight, GUILayout.Width (46)))
                    reaktor.StopOverride();
            }
            else
            {
                if (GUILayout.Button ("Select", EditorStyles.miniButtonRight, GUILayout.Width (46)))
                    Selection.activeGameObject = reaktor.gameObject;
            }

            EditorGUILayout.EndHorizontal ();
        }

        EditorGUILayout.EndScrollView ();
    }
}

} 
