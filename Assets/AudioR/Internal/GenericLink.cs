
using UnityEngine;
using System.Collections;

namespace Reaktion {

// Placeholder class to share the property drawer with specialized link classes.
public class GenericLinkBase {}

// A class used to link injectors/reaktors/gears.
public class GenericLink<T> : GenericLinkBase where T : Component
{
    // Link mode.
    public enum Mode { Null, Automatic, ByReference, ByName }

    [SerializeField] Mode _mode = Mode.Automatic;

    public Mode mode {
        get { return _mode; }
        set { _mode = value; Update(); }
    }

    // Link-by-reference mode information.
    [SerializeField] T _reference;

    public T reference {
        get { return _reference; }
        set { _reference = value; Update(); }
    }

    // Link-by-name mode information.
    [SerializeField] string _name;

    public string name {
        get { return _name; }
        set { _name = value; Update(); }
    }

    // "Update the link" flag (exposed only for Editor).
    [SerializeField] bool _forceUpdate;

    // Master script.
    MonoBehaviour master;

    // Linked object.
    T _linkedObject;

    public T linkedObject {
        get {
            if (_forceUpdate) Update();
            return _linkedObject;
        }
    }

    // Initialization (should be called from the master script).
    public void Initialize(MonoBehaviour master)
    {
        this.master = master;
        Update();
    }

    // Update the link.
    public void Update()
    {
        _linkedObject = FindLinkedObject();
        _forceUpdate = false;
    }

    // Find the linked object.
    T FindLinkedObject()
    {
        if (_mode == Mode.Automatic && master)
        {
            var r = master.GetComponent<T>();
            if (r) return r;

            r = master.GetComponentInParent<T>();
            if (r) return r;

            r = master.GetComponentInChildren<T>();
            if (r) return r;

            return Object.FindObjectOfType<T>();
        }

        if (_mode == Mode.ByReference) return _reference;

        if (_mode == Mode.ByName)
        {
            var go = GameObject.Find(_name);
            if (go) return go.GetComponent<T>();
        }

        return null;
    }
}

} 
