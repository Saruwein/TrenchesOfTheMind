using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StoryPoint : MonoBehaviour
{
    // has this storyPoint been traversed?
    public bool isPassed;
    [SerializeField]
    private bool _isCutScene = false;
    // Lis of prerequisite StoryPoints that have to be passed
    public List<StoryPoint> prereqs = new List<StoryPoint>();
    public VideoClip clip;

    private bool _debug = true;

    public StoryPoint()
    {
        this.name = "";
        isPassed = false;
    }

    public StoryPoint(string name, bool isPassed)
    {
        this.name = name;
        this.isPassed = isPassed;
    }

    /// <summary>
    /// return whether or not the 
    /// </summary>
    public bool IsCutScene { get { return _isCutScene; } }

    /// <summary>
    /// flag the storyPoint as dealt with
    /// </summary>
    public void True()
    {
        if (CheckPrereqs())
        {
            isPassed = true;
            gameObject.SetActive(false);
            if (_debug) { Debug.Log($"Passed StoryPoint {name}"); }
        }
        
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

    private void Awake()
    {
        Button button = GetComponent<Button>();

        button?.onClick.AddListener(True);
    }


}
