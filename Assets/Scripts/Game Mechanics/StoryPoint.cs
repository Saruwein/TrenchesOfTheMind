using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StoryPoint : MonoBehaviour
{
    // has this storyPoint been traversed?
    public bool isPassed;

    [Tooltip("Required StoryPoints to be passed")]
    public List<StoryPoint> prereqs = new List<StoryPoint>();
    [Tooltip("GameObjects of which the visibility is depending on this to be passed")]
    public List<GameObject> followUpGOs = new List<GameObject>();
    private View _view = null;

    [Space]
    [Tooltip("Leave Emnpty if not a CutScene")]
    public VideoClip clip;

    private bool _debug = true;

    private void Awake()
    {
        Button button = GetComponent<Button>();
        button?.onClick.AddListener(True);

        _view = transform.parent.GetComponentInChildren<View>();
    }

    /// <summary>
    /// return whether or not the 
    /// </summary>
    public bool IsCutScene { get { return clip != null; } }

    /// <summary>
    /// flag the storyPoint as dealt with
    /// </summary>
    public void True()
    {
        // check if we all requirements are met
        if (CheckPrereqs())
        {
            // handle CutScene StoryPoints
            if (IsCutScene)
            {
                // hide anything that may overlay
                if (_debug) { Debug.Log($"SP {name} is CutScene and plays {clip}"); }
                GetComponent<Image>().enabled = false;
                GetComponent<Button>().enabled = false;
                _view.ToggleNavBtnVis(false);

                // play clip
                VideoPlayer vp = GetComponent<VideoPlayer>();
                vp.clip = clip;
                vp.enabled = true;
                vp.Play();

                // set stuff to be visible again
                vp.loopPointReached += (_) => gameObject.SetActive(false);
                vp.loopPointReached += (_) => _view.ToggleNavBtnVis(true);
            }
            
            // pass StoryPoint
            isPassed = true;
            gameObject.SetActive(false);

            // handle followUp StoryPoints
            if (followUpGOs.Count > 0) 
            { 
                if (_debug) { Debug.Log($"SP {name} shows {followUpGOs.Count} followUps"); }
                displayFollowUpGOs(true);

                // enable back button, set it up to disable SPs and disappear
                Button back = _view.NavManager.BtnBack;
                back.onClick.RemoveAllListeners();
                back.onClick.AddListener(() => displayFollowUpGOs(false));
                back.onClick.AddListener(() => back.gameObject.SetActive(false));
                back.gameObject.SetActive(true);
            }
            
            if (_debug) { Debug.Log($"Passed StoryPoint {name}"); }
        }
        
    }

    // check if requirements are met
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

    /// <summary>
    /// display FollowUpGOs
    /// </summary>
    /// <param name="toggle"></param>
    public void displayFollowUpGOs(bool toggle)
    {        
        foreach (GameObject obj in followUpGOs) 
        { 
            obj.SetActive(toggle); 
        }
    }

    


}
