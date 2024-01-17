
using UnityEngine;
using System.Collections;

namespace Reaktion {

[System.Serializable]
public class Modifier
{
    public bool enabled;
    public float min = 0;
    public float max = 1;
    public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

    public float Evaluate(float i)
    {
        return Mathf.Lerp(min, max, curve.Evaluate(i));
    }

    public static Modifier Linear(float min, float max)
    {
        var instance = new Modifier();
        instance.min = min;
        instance.max = max;
        return instance;
    }
}

} 
