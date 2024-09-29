using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

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


    private void Start()
    {
        cutScenes.loopPointReached += AutomatedProgressPoint;
    }

    /// <summary>
    /// Start CutScene
    /// </summary>
    public void PlayCut()
    {
        cutScenes.clip = _cutSceneMP4s[cutSceneProgress];
        cutScenes.enabled = true;
        cutScenes.Play();

    }

    /// <summary>
    /// track progress, 
    /// </summary>
    /// <param name="vp"></param>
    public void AutomatedProgressPoint(VideoPlayer vp)
    {
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
    }

    public void ZoomIn()
    {
        navManager.ZoomIn();
    }




}
