using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StoryPoint : MonoBehaviour
{
    // has this storyPoint been traversed?
    public bool isPassed;
    // Lis of prerequisite StoryPoints that have to be passed
    public List<StoryPoint> prereqs = new List<StoryPoint>();

    public StoryPoint(string name)
    {
        this.name = name;
        isPassed = false;
    }

    public StoryPoint(string name, bool isPassed)
    {
        this.name = name;
        this.isPassed = isPassed;
    }

    /// <summary>
    /// flag the storyPoint as dealt with
    /// </summary>
    public void True()
    {
        if (CheckPrereqs())
        {
            isPassed = true;
            gameObject.SetActive(false);
        }
        Debug.Log($"Passed StoryPoint {name}");
    }

    public bool CheckPrereqs()
    {
        bool prereqMet = true;
        if (prereqs.Count > 0)
        {
            foreach (StoryPoint sp in prereqs)
            {
                prereqMet = prereqMet && sp.isPassed;
                
            }
        }
        if (!prereqMet) { Debug.Log($"Storypoint {name} is missing at least one prerequisite"); }
        return prereqMet;
    }

}
