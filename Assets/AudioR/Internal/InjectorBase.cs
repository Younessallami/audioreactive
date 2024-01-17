
using UnityEngine;
using System.Collections;

namespace Reaktion {

public class InjectorBase : MonoBehaviour
{
    protected float dbLevel = -60.0f;

    public float DbLevel
    {
        get { return dbLevel; }
    }
}

} 
