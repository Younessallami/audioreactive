
using UnityEngine;
using UnityEditor;
using System.Runtime.InteropServices;

namespace OscJack
{
    // OSC message monitor window
    class OscJackWindow : EditorWindow
    {
        #region Custom Editor Window Code

        [MenuItem("Window/OSC Jack")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow<OscJackWindow>("OSC Jack");
        }

        void OnGUI()
        {
            EditorGUILayout.BeginVertical();

            foreach (var item in OscMaster.MasterDirectory)
            {
                var data = item.Value;
                var text = "";

                for (var i = 0; i < data.Length - 1; i++)
                    text += data[i] + ", ";
                text += data[data.Length - 1];

                EditorGUILayout.LabelField(item.Key, text);
            }

            EditorGUILayout.EndVertical();
        }

        #endregion

        #region Update And Repaint

        const int _updateInterval = 20;
        int _countToUpdate;
        int _lastMessageCount;

        void Update()
        {
            if (--_countToUpdate > 0) return;
            _countToUpdate = _updateInterval;

            var mcount = OscMaster.MasterDirectory.TotalMessageCount;
            if (mcount != _lastMessageCount) {
                Repaint();
                _lastMessageCount = mcount;
            }
        }

        #endregion
    }
}
