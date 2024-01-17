
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Animator Gear")]
public class AnimatorGear : MonoBehaviour
{
    public ReaktorLink reaktor;
    public Modifier speed;
    public Trigger trigger;
    public string triggerName;

    Animator animator;

    void Awake()
    {
        reaktor.Initialize(this);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (speed.enabled)
            animator.speed = speed.Evaluate(reaktor.Output);
        if (trigger.Update(reaktor.Output))
            animator.SetTrigger(triggerName);
    }
}

} 
