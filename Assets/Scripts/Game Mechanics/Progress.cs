using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;


public class Progress : MonoBehaviour
{
    [Header("Scripts")]
    public NavManager navManager;
    public VideoPlayer vidPlayer;
    public SceneHandler sceneHandler;
    [Space]

    [Header("Assets")]
    [SerializeField]
    private List<VideoClip> _cutSceneMP4s = new List<VideoClip>();
    [SerializeField]
    List<GameObject> _views = new List<GameObject>();
    
    [Space]
    public int view = 0;
    public int cutSceneProgress = 0;
    private StoryPoint _sp = new StoryPoint();

    [Space]
    public List<Sprite> symbols = new List<Sprite>();
    private int[] _symbols = new int[3];

    private bool _debug = true;


    /*


// Video Sequences

    private void Start()
    {
        PlayCut(_cutSceneMP4s[cutSceneProgress]);
    }

    /// <summary>
    /// Start CutScene
    /// </summary>
    public void PlayCut(VideoClip clip)
    {
        // setup VideoPlayer
        vidPlayer.clip = clip;
        vidPlayer.loopPointReached += OnCutSceneEnd;
        // play CutScene
        Debug.Log($"New CutScene: {vidPlayer.clip.name}");
        vidPlayer.enabled = true;
        vidPlayer.Play();

    }

    public void PlayClip(VideoClip clip, StoryPoint sp)
    {
        // store StoryPoint in temp variable
        _sp = sp;
        // setup VideoPlayer
        vidPlayer.clip = clip;
        vidPlayer.loopPointReached += OnVideoClipEnd;
        // play CutScene
        Debug.Log($"New CutScene: {vidPlayer.clip.name}");
        vidPlayer.enabled = true;
        vidPlayer.Play();
    }

    /// <summary>
    /// ends Video 
    /// </summary>
    /// <param name="vp"></param>
    public void OnCutSceneEnd(VideoPlayer vp)
    {
        // disable Videoplayer
        if (_debug) { Debug.Log($"End Video {vidPlayer.clip.name}"); }
        vidPlayer.loopPointReached -= OnCutSceneEnd;
        vp.enabled = false;
    }
    /// <summary>
    /// ends Video and triggers sp.True()
    /// </summary>
    /// <param name="vp"></param>
    /// <param name="sp"></param>
    public void OnVideoClipEnd(VideoPlayer vp)
    {
        // progress
        _sp.True();
        // disable VideoPlayer
        vidPlayer.loopPointReached -= OnVideoClipEnd;
        if (_debug) { Debug.Log($"End Video {vidPlayer.clip.name}"); }
        vp.enabled = false;
    }


// Navigation

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
        if (IsAutoCutScene())
        {
            cutSceneProgress++;
            PlayCut(_cutSceneMP4s[cutSceneProgress]);                 
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
        
        if (IsAutoCutScene())
        {
            cutSceneProgress--;
            PlayCut(_cutSceneMP4s[cutSceneProgress]);
        }
    }

    private bool IsAutoCutScene()
    {
        StoryPoint sp = _views[view].GetComponent<View>().storyPoints[0];
        return sp.IsCutScene && sp.CheckPrereqs() && !sp.isPassed;
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
            vidPlayer.loopPointReached += null;
            vidPlayer.loopPointReached += End;

            cutSceneProgress = _cutSceneMP4s.Count - 1;
            PlayCut(_cutSceneMP4s[cutSceneProgress]);

            sceneHandler.ChangeScene("TitleScene");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="i"></param>
    /// <returns></returns>
    public List<Sprite> GetLockingWheel(int i)
    {
        List<Sprite> wheel = new List<Sprite>(symbols);
        for (int k = 0; k < 3; k++)
        {
            if (k != i) { wheel.RemoveAt(_symbols[k]); }
        }
        return wheel;
    }


    /// <summary>
    /// draw three unique characters 
    /// </summary>
    private void BuildLock()
    {
        _symbols[0] = RandomLock();
        do { _symbols[1] = RandomLock();  } while (_symbols[1] == _symbols[0]);
        do { _symbols[2] = RandomLock(); } while (_symbols[2] == _symbols[0] || _symbols[2] == _symbols[1]);
    }

    /// <summary>
    /// Draw random number fromlocks
    /// </summary>
    /// <returns></returns>
    private int RandomLock()
    {
        double d = Random.Range(0, 1);
        return (int) Mathf.Round((float) d * symbols.Count - 1);
    }


    /// <summary>
    /// End Game // return to Title Screen
    /// </summary>
    public void End(VideoPlayer vp) { SceneHandler.ReturnTitle(); }



    */

}
