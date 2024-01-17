
using UnityEngine;
using System.Collections;

namespace Reaktion {

[System.Serializable]
public class Trigger
{
    public bool enabled;
    public float threshold = 0.5f;
    public float interval = 0.1f;

    float previous;
    float timer;

    public bool Update(float current)
    {
        if (!enabled) return false;

        if (timer <= 0.0f && current >= threshold && previous < threshold)
        {
            // bang
            timer = interval;
            previous = current;
            return true;
        }
        else
        {
            // stay
            timer -= Time.deltaTime;
            previous = current;
            return false;
        }
    }
}

} 
