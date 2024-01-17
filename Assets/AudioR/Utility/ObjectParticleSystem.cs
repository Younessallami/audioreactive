
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Utility/Object Particle System")]
[RequireComponent(typeof(ParticleSystem))]
public class ObjectParticleSystem : MonoBehaviour
{
    public GameObject prefab;
    public int maxParticles = 100;

    ParticleSystem.Particle[] particles;
    GameObject[] pool;

    void Start()
    {
        var count = Mathf.Min(maxParticles, GetComponent<ParticleSystem>().maxParticles);

        particles = new ParticleSystem.Particle[count];
        pool = new GameObject[count];

        for (var i = 0; i < count; i++)
            pool[i] = Instantiate(prefab) as GameObject;
    }

    void LateUpdate()
    {
        var count = GetComponent<ParticleSystem>().GetParticles(particles);

        for (var i = 0; i < count; i++)
        {
            var p = particles [i];
            var o = pool[i];

            o.GetComponent<Renderer>().enabled = true;

            o.transform.position = prefab.transform.position + p.position;
            o.transform.localRotation = Quaternion.AngleAxis(p.rotation, p.axisOfRotation) * prefab.transform.rotation;
            o.transform.localScale = prefab.transform.localScale * p.size;
        }

        for (var i = count; i < pool.Length; i++)
            pool[i].GetComponent<Renderer>().enabled = false;
    }
}

} 
