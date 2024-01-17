
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Global Setting Gear")]
public class GlobalSettingGear : MonoBehaviour
{
    public ReaktorLink reaktor;
    public AnimationCurve timeScaleCurve = AnimationCurve.Linear(0, 0.2f, 1, 1);

    void OnDisable()
    {
        Time.timeScale = 1;
    }

    void Awake()
    {
        reaktor.Initialize(this);
    }

    void Update()
    {
        Time.timeScale = timeScaleCurve.Evaluate(reaktor.Output);
    }
}

} 
