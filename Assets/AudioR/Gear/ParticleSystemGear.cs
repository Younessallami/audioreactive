
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Particle System Gear")]
public class ParticleSystemGear : MonoBehaviour
{
    public ReaktorLink reaktor;

    public Trigger burst;
    public int burstNumber = 10;

    public Modifier emissionRate = Modifier.Linear(0, 20);

    public Modifier size = Modifier.Linear(0.5f, 1.5f);

    ParticleSystem.Particle[] tempArray;
    
    void Awake()
    {
        reaktor.Initialize(this);
    }

    void Update()
    {
        if (burst.Update(reaktor.Output))
        {
            GetComponent<ParticleSystem>().Emit(burstNumber);
            GetComponent<ParticleSystem>().Play();
        }

        if (emissionRate.enabled)
            GetComponent<ParticleSystem>().emissionRate = emissionRate.Evaluate(reaktor.Output);

        if (size.enabled)
            ResizeParticles(size.Evaluate(reaktor.Output));
    }

    void ResizeParticles(float newSize)
    {
        if (tempArray == null || tempArray.Length != GetComponent<ParticleSystem>().maxParticles)
            tempArray = new ParticleSystem.Particle[GetComponent<ParticleSystem>().maxParticles];

        var count = GetComponent<ParticleSystem>().GetParticles(tempArray);

        for (var i = 0; i < count; i++)
            tempArray[i].size = newSize;

        GetComponent<ParticleSystem>().SetParticles(tempArray, count);
    }
}

} 
