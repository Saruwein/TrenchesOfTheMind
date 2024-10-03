using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Progress : MonoBehaviour
{
    [Header("Scripts")]
    public NavManager navManager;
    public VideoPlayer cutScenes;

    [Header("Assets")]
    [SerializeField]
    private List<VideoClip> _cutSceneMP4s = new List<VideoClip>();
    [SerializeField]
    List<GameObject> _views = new List<GameObject>();

    [Space]
    public int view = 0;
    public int cutSceneProgress = 0;

    private bool _debug = true;

// Video Sequences

    private void Start()
    {
        cutScenes.loopPointReached += AutomatedProgressPoint;
    }

    /// <summary>
    /// Start CutScene
    /// </summary>
    public void PlayCut()
    {
        // play cutscene
        cutScenes.clip = _cutSceneMP4s[cutSceneProgress];
        Debug.Log($"New CutScene: {cutScenes.clip.name}");
        cutScenes.enabled = true;
        cutScenes.Play();

    }

    /// <summary>
    /// track progress, 
    /// </summary>
    /// <param name="vp"></param>
    public void AutomatedProgressPoint(VideoPlayer vp)
    {
        // progress
        _cutSceneMP4s[cutSceneProgress].GetComponent<StoryPoint>().True();
        if (_debug) { Debug.Log($"End Video {cutScenes.clip.name}"); }
        
        // set up next CutScene
        cutSceneProgress++;
        vp.enabled = false;
    }





    /// <summary>
    /// navigate left
    /// </summary>
    public void ScreenLeft()
    {
        // change view
        if (view > 0) { view--; }
        else { view = 3; }    
        // determine visible view    
        for (int i = 0; i < 4; i++)
        {
            _views[i].SetActive(i == view);
            if (_debug && _views[i].activeSelf) { Debug.Log($"Perspecive: {_views[i]}"); }
        }    

        // check for video condition 
        // {does it have cutscenes, are prereqs met, and has it been played?}        
        StoryPoint sp = _views[view].GetComponent<View>().storyPoints[0];    
        if ((sp.name.StartsWith('1') || sp.name.StartsWith('3')) && sp.CheckPrereqs() && !sp.isPassed)
        {
            PlayCut();
                 
        }
    }

    /// <summary>
    /// navigate right
    /// </summary>
    public void ScreenRight() 
    {
        // change view
        if (view < 3) { view++; }
        else { view = 0; }
        // determine visible view
        for (int i = 0; i < 4; i++)
        {
            _views[i].SetActive(i == view);
        }

        // check for video condition 
        // {does it have cutscenes, are prereqs met, and has it been played?}
        StoryPoint sp = _views[view].GetComponent<View>().storyPoints[0];
        if ((sp.name.StartsWith('1') || sp.name.StartsWith('3')) && sp.CheckPrereqs() && !sp.isPassed)
        {
            PlayCut();
        }
    }

    public void ZoomIn()
    {
        navManager.ZoomIn();
    }

// End Sequence

    /// <summary>
    /// test lock combination --> end game
    /// </summary>
    /// <param name="Lock"></param>
    public void TryLock(Lock Lock)
    {
        if (!Lock.isLocked())
        {
            cutScenes.loopPointReached += null;
            cutScenes.loopPointReached += End;

            cutSceneProgress = _cutSceneMP4s.Count - 1;
            PlayCut();
        }
    }

    /// <summary>
    /// End Game // return to Title Screen
    /// </summary>
    public void End(VideoPlayer vp) { SceneHandler.ReturnTitle(); }

}
