
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Jitter Motion Gear")]
public class JitterMotionGear : MonoBehaviour
{
    public ReaktorLink reaktor;
    public Modifier positionFrequency = Modifier.Linear(0, 0.1f);
    public Modifier rotationFrequency = Modifier.Linear(0, 0.1f);
    public Modifier positionAmount = Modifier.Linear(0, 1);
    public Modifier rotationAmount = Modifier.Linear(0, 30);

    JitterMotion jitter;

    void Awake()
    {
        reaktor.Initialize(this);
        jitter = GetComponent<JitterMotion>();
    }

    void Update()
    {
        var o = reaktor.Output;

        if (positionFrequency.enabled)
            jitter.positionFrequency = positionFrequency.Evaluate(o);

        if (rotationFrequency.enabled)
            jitter.rotationFrequency = rotationFrequency.Evaluate(o);

        if (positionAmount.enabled)
            jitter.positionAmount = positionAmount.Evaluate(o);

        if (rotationAmount.enabled)
            jitter.rotationAmount = rotationAmount.Evaluate(o);
    }
}

} 
