
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Generic Trigger Gear")]
public class GenericTriggerGear : MonoBehaviour
{
    public ReaktorLink reaktor;
    public float threshold = 0.9f;
    public float interval = 0.1f;
    public UnityEvent target;

    float previousOutput;
    float triggerTimer;

    void Awake()
    {
        reaktor.Initialize(this);
    }

    void Update()
    {
        if (triggerTimer <= 0.0f && reaktor.Output >= threshold && previousOutput < threshold)
        {
            target.Invoke();
            triggerTimer = interval;
        }
        else
        {
            triggerTimer -= Time.deltaTime;
        }
        previousOutput = reaktor.Output;
    }
}

} 
