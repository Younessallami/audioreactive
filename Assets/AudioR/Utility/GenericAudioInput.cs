
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Utility/Generic Audio Input")]
public class GenericAudioInput : MonoBehaviour
{
    AudioSource audioSource;

    public float estimatedLatency { get; protected set; }

    void Awake()
    {
        // Create an audio source.
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = true;

        StartInput();
    }

    void OnApplicationPause(bool paused)
    {
        if (paused)
        {
            audioSource.Stop();
            Microphone.End(null);
            audioSource.clip = null;
        }
        else
            StartInput();
    }

    void StartInput()
    {
        var sampleRate = AudioSettings.outputSampleRate;

        // Create a clip which is assigned to the default microphone.
        audioSource.clip = Microphone.Start(null, true, 1, sampleRate);

        if (audioSource.clip != null)
        {
            // Wait until the microphone gets initialized.
            int delay = 0;
            while (delay <= 0) delay = Microphone.GetPosition(null);

            // Start playing.
            audioSource.Play();

            // Estimate the latency.
            estimatedLatency = (float)delay / sampleRate;
        }
        else
            Debug.LogWarning("GenericAudioInput: Initialization failed.");
    }
}

} 
