
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Generic Gradient Gear")]
public class GenericGradientGear : MonoBehaviour
{
    [System.Serializable]
    public class ColorEvent : UnityEvent<Color> {}

    public ReaktorLink reaktor;
    public Gradient gradient;
    public ColorEvent target;

    void Awake()
    {
        reaktor.Initialize(this);
        UpdateTarget(0);
    }

    void Update()
    {
        UpdateTarget(reaktor.Output);
    }

    void UpdateTarget(float param)
    {
        target.Invoke(gradient.Evaluate(param));
    }
}

} 
