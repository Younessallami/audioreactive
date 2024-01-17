
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Gear/Material Gear")]
public class MaterialGear : MonoBehaviour
{
    public enum TargetType { Color, Float, Vector, Texture }

    public ReaktorLink reaktor;

    public int materialIndex;

    public string targetName = "_Color";
    public TargetType targetType = TargetType.Color;

    public float threshold = 0.5f;

    public Gradient colorGradient;

    public AnimationCurve floatCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public Vector4 vectorFrom = Vector4.zero;
    public Vector4 vectorTo = Vector4.one;

    public Texture textureLow;
    public Texture textureHigh;

    Material material;

    void Awake()
    {
        reaktor.Initialize(this);

        if (materialIndex == 0)
            material = GetComponent<Renderer>().material;
        else
            material = GetComponent<Renderer>().materials[materialIndex];

        UpdateMaterial(0);
    }

    void Update()
    {
        UpdateMaterial(reaktor.Output);
    }

    void UpdateMaterial(float param)
    {
        switch (targetType)
        {
        case TargetType.Color:
            material.SetColor(targetName, colorGradient.Evaluate(param));
            break;
        case TargetType.Float:
            material.SetFloat(targetName, floatCurve.Evaluate(param));
            break;
        case TargetType.Vector:
            material.SetVector(targetName, Vector4.Lerp(vectorFrom, vectorTo, param));
            break;
        case TargetType.Texture:
            material.SetTexture(targetName, param < threshold ? textureLow : textureHigh);
            break;
        }
    }
}

} 
