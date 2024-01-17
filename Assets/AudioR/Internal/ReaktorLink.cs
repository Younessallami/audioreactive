
using UnityEngine;
using System.Collections;

namespace Reaktion {

// A class used to reference a Reaktor from gears.
[System.Serializable]
public class ReaktorLink : GenericLink<Reaktor>
{
    // Get a output value from the Reaktor.
    public float Output {
        get {
            return linkedObject ? linkedObject.Output : 0.0f;
        }
    }
}

} 
