
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Generic Curve Gear")]
public class GenericCurveGear : MonoBehaviour
{
    public enum OptionType { Bool, Int, Float, Vector }

    [System.Serializable] public class BoolEvent : UnityEvent<bool> {}
    [System.Serializable] public class IntEvent : UnityEvent<int> {}
    [System.Serializable] public class FloatEvent : UnityEvent<float> {}
    [System.Serializable] public class VectorEvent : UnityEvent<Vector3> {}

    public ReaktorLink reaktor;

    public OptionType optionType = OptionType.Float;
    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);
    public Vector3 origin;
    public Vector3 direction = Vector3.right;

    public BoolEvent boolTarget;
    public IntEvent intTarget;
    public FloatEvent floatTarget;
    public VectorEvent vectorTarget;

    void Awake()
    {
        reaktor.Initialize(this);
    }

    void Update()
    {
        var o = curve.Evaluate(reaktor.Output);
        if (optionType == OptionType.Bool)
        {
            boolTarget.Invoke(0.5f <= o);
        }
        else if (optionType == OptionType.Int)
        {
            intTarget.Invoke((int)o);
        }
        else if (optionType == OptionType.Vector)
        {
            vectorTarget.Invoke(origin + direction * o);
        }
        else
        {
            floatTarget.Invoke(o);
        }
    }
}

} 