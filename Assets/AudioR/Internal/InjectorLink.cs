
using UnityEngine;
using System.Collections;

namespace Reaktion {

// A class used to reference an Injector from Reaktors.
[System.Serializable]
public class InjectorLink : GenericLink<InjectorBase>
{
    // Get a output dB level from the Injector.
    public float DbLevel {
        get {
            return linkedObject ? linkedObject.DbLevel : -1e12f;
        }
    }
}

} 
