
using UnityEngine;
using OscJack;

namespace Reaktion
{
    [AddComponentMenu("Reaktion/Injector/OSC Injector")]
    public class OscInjector : InjectorBase
    {
        public enum ScaleMode { Linear01, Decibel };
        public ScaleMode scaleMode;
        public string address = "/reaktion";
        public int dataIndex = 0;

        void Update()
        {
            System.Object[] data = OscMaster.GetData(address);
            if (data == null) return;

            var level = (float)data[dataIndex];

            if (scaleMode == ScaleMode.Linear01)
            {
                const float refLevel = 0.70710678118f; // 1/sqrt(2)
                const float zeroOffs = 1.5849e-13f;
                level = Mathf.Clamp01(level);
                dbLevel = Mathf.Log(level / refLevel + zeroOffs, 10) * 20;
            }
            else
            {
                dbLevel = Mathf.Min(level, 0.0f);
            }
        }
    }
}
