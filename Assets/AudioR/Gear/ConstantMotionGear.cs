
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Constant Motion Gear")]
public class ConstantMotionGear : MonoBehaviour
{
    public ReaktorLink reaktor;
    public Modifier position = Modifier.Linear(0, 1);
    public Modifier rotation = Modifier.Linear(0, 30);

    ConstantMotion motion;

    void Awake()
    {
        reaktor.Initialize(this);
        motion = GetComponent<ConstantMotion>();
    }

    void Update()
    {
        if (position.enabled)
            motion.position.velocity = position.Evaluate(reaktor.Output);
        if (rotation.enabled)
            motion.rotation.velocity = rotation.Evaluate(reaktor.Output);
    }
}

} 
