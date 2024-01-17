
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Light Gear")]
public class LightGear : MonoBehaviour
{
    public ReaktorLink reaktor;
    public Modifier intensity;
    public bool enableColor;
    public Gradient colorGradient;

    void Awake()
    {
        reaktor.Initialize(this);
        UpdateLight(0);
    }

    void Update()
    {
        UpdateLight(reaktor.Output);
    }

    void UpdateLight(float param)
    {
        if (intensity.enabled)
            GetComponent<Light>().intensity = intensity.Evaluate(param);
        if (enableColor)
            GetComponent<Light>().color = colorGradient.Evaluate(param);
    }
}

} 
