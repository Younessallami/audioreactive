
using UnityEngine;
using System.Collections;
using MidiJack;

namespace Reaktion {

[AddComponentMenu("Reaktion/Injector/Beat Injector")]
public class BeatInjector : InjectorBase
{
    public float bpm = 120;
    public AnimationCurve curve = AnimationCurve.Linear(0, 1, 0.5f, 0);

    public int tapNote = -1;
    public MidiChannel tapChannel = MidiChannel.All;
    public string tapButton;

    float time;
    float tapTime;

    void Update()
    {
        if (tapNote >= 0)
            if (MidiMaster.GetKeyDown(tapChannel, tapNote))
                Tap();

        if (!string.IsNullOrEmpty(tapButton))
            if (Input.GetButtonDown(tapButton))
                Tap();

        var interval = 60.0f / bpm;

        time = (time + Time.deltaTime) % interval;

        dbLevel = (curve.Evaluate(time / interval) - 1) * 18;
    }

    public void Tap()
    {
        var delta = Time.time - tapTime;
        if (tapTime > 0.2f && delta < 3.0f)
        {
            bpm = Mathf.Lerp(bpm, 60.0f / delta, 0.15f);
            time = (time > 0.2f) ? 0.0f : time * 0.5f;
        }
        tapTime = Time.time;
    }
}

} 
