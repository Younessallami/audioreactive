
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Camera Gear")]
public class CameraGear : MonoBehaviour
{
    public ReaktorLink reaktor;
    public Modifier fieldOfView = Modifier.Linear(60, 45);
    public Modifier viewportWidth = Modifier.Linear(1, 0.2f);
    public Modifier viewportHeight = Modifier.Linear(1, 0.2f);

    void Awake()
    {
        reaktor.Initialize(this);
    }

    void Update()
    {
        if (fieldOfView.enabled)
            GetComponent<Camera>().fieldOfView = fieldOfView.Evaluate(reaktor.Output);

        if (viewportWidth.enabled || viewportHeight.enabled)
        {
            var rect = GetComponent<Camera>().rect;
            if (viewportWidth.enabled)
            {
                rect.width = viewportWidth.Evaluate(reaktor.Output);
                rect.x = (1.0f - rect.width) * 0.5f;
            }
            if (viewportHeight.enabled)
            {
                rect.height = viewportHeight.Evaluate(reaktor.Output);
                rect.y = (1.0f - rect.height) * 0.5f;
            }
            GetComponent<Camera>().rect = rect;
        }
    }
}

} 
