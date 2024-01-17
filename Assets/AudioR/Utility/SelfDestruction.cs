
using UnityEngine;
using System.Collections;

namespace Reaktion {

[AddComponentMenu("Reaktion/Utility/Self Destruction")]
public class SelfDestruction : MonoBehaviour
{
    public enum ConditionType { Distance, Bounds, Time, ParticleSystem }
    public enum ReferenceType { Origin, Point, InitialPosition, GameObject, GameObjectName }

    public ConditionType conditionType = ConditionType.Distance;
    public ReferenceType referenceType = ReferenceType.InitialPosition;

    public float maxDistance = 10;
    public Bounds bounds = new Bounds(Vector3.zero, new Vector3(10, 10, 10));
    public float lifetime = 5;

    public Vector3 referencePoint;
    public GameObject referenceObject;
    public string referenceName;

    float timer;
    Vector3 initialPoint;
    GameObject referenceObjectCache; // used to dereference referenceName

    Vector3 GetReferencePoint()
    {
        bool runtime = Application.isPlaying;

        if (referenceType == ReferenceType.Point)
            return referencePoint;

        if (referenceType == ReferenceType.InitialPosition)
            return runtime ? initialPoint : transform.position;

        if (referenceType == ReferenceType.GameObject)
            if (referenceObject != null)
                return referenceObject.transform.position;

        if (referenceType == ReferenceType.GameObjectName)
        {
            if (!runtime || referenceObjectCache == null)
                referenceObjectCache = GameObject.Find(referenceName);
            if (referenceObjectCache != null)
                return referenceObjectCache.transform.position;
        }

        // Default reference point
        return Vector3.zero;
    }

    bool IsAlive()
    {
        if (conditionType == ConditionType.Distance)
            return Vector3.Distance(transform.position, GetReferencePoint()) <= maxDistance;

        if (conditionType == ConditionType.Bounds)
            return bounds.Contains(transform.position - GetReferencePoint());

        if (conditionType == ConditionType.Time)
            return timer < lifetime;

        // conditionType == ConditionType.ParticleSystem:
        return GetComponent<ParticleSystem>() != null && GetComponent<ParticleSystem>().IsAlive();
    }

    void Start()
    {
        initialPoint = transform.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    void LateUpdate() 
    {
        if (!IsAlive()) Object.Destroy(gameObject);   
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        if (conditionType == ConditionType.Distance)
            Gizmos.DrawWireSphere(GetReferencePoint(), maxDistance);

        if (conditionType == ConditionType.Bounds)
            Gizmos.DrawWireCube(GetReferencePoint() + bounds.center, bounds.size);
    }
}

} 
