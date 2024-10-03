using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public List<LockWheel> wheels = new List<LockWheel>();

    /// <summary>
    /// returns whether or not door is locked
    /// </summary>
    /// <returns></returns>
    public bool isLocked()
    {
        bool l = false;
        string debug = "Lockstate: ";
        // iterate through wheels and check if intended == selected
        for (int i = 0; i < wheels.Count; i++)
        {
            debug += $"\nwheel[{i}] returns {wheels[i].selected != wheels[i].intended}";
            if (wheels[i].selected != wheels[i].intended) { l = true; break; }
        }
        Debug.Log(debug + $"\nLockstate {l}");
        return l;
    }
}
