
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Transform Gear")]
public class TransformGear : MonoBehaviour
{
    public enum TransformMode {
        Off, XAxis, YAxis, ZAxis, Arbitrary, Random
    };

    // A class for handling each transformation.
    [System.Serializable]
    public class TransformElement
    {
        public TransformMode mode = TransformMode.Off;

        // Basic modifier parameters.
        public float min = 0;
        public float max = 1;
        public AnimationCurve curve = AnimationCurve.Linear(0, 0, 1, 1);

        // Used only in the arbitrary mode.
        public Vector3 arbitraryVector = Vector3.up;

        // Affects lerp parameter.
        public float randomness = 0;

        // Randomizer states.
        Vector3 randomVector;
        float randomScalar;

        public void Initialize()
        {
            randomVector = Random.onUnitSphere;
            randomScalar = Random.value;
        }

        // Get a vector corresponds to the current transform mode.
        public Vector3 Vector {
            get {
                switch (mode)
                {
                    case TransformMode.XAxis:     return Vector3.right;
                    case TransformMode.YAxis:     return Vector3.up;
                    case TransformMode.ZAxis:     return Vector3.forward;
                    case TransformMode.Arbitrary: return arbitraryVector;
                    case TransformMode.Random:    return randomVector;
                }
                return Vector3.zero;
            }
        }

        // Evaluate the scalar function.
        public float GetScalar(float x)
        {
            var scale = (1.0f - randomness * randomScalar);
            return Mathf.Lerp(min, max, scale * curve.Evaluate(x));
        }
    }

    public ReaktorLink reaktor;

    public TransformElement position = new TransformElement();
    public TransformElement rotation = new TransformElement{ max = 90 };
    public TransformElement scale = new TransformElement{ arbitraryVector = Vector3.one, min = 1, max = 2 };

    public bool addInitialValue = true;

    Vector3 initialPosition;
    Quaternion initialRotation;
    Vector3 initialScale;

    void Awake()
    {
        reaktor.Initialize(this);
        position.Initialize();
        rotation.Initialize();
        scale.Initialize();
    }

    void OnEnable()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        initialScale = transform.localScale;
    }

    void Update()
    {
        var ro = reaktor.Output;

        if (position.mode != TransformMode.Off)
        {
            transform.localPosition = position.Vector * position.GetScalar(ro);
            if (addInitialValue) transform.localPosition += initialPosition;
        }

        if (rotation.mode != TransformMode.Off)
        {
            transform.localRotation = Quaternion.AngleAxis(rotation.GetScalar(ro), rotation.Vector);
            if (addInitialValue) transform.localRotation *= initialRotation;
        }

        if (scale.mode != TransformMode.Off)
        {
            var s = Vector3.one + scale.Vector * (scale.GetScalar(ro) - 1);
            transform.localScale = Vector3.Scale(addInitialValue ? initialScale : Vector3.one, s);
        }
    }
}

} 
