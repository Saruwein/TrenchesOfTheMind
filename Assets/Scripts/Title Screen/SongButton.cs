using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SongButton : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioPlayer;
    [Space]
    public Sprite lockSprite;
    public Sprite playSprite;
    public TMP_FontAsset regular;
    public TMP_FontAsset bold;
    [Space]
    [SerializeField]
    private int _trackNum = 0;
    [SerializeField]
    private AudioClip _track;

    private Image _image;
    private TextMeshPro _tmp;

    private Color _white60 = new Color(1f, 1f, 1f, .6f);
    private Color _white85 = new Color(1f, 1f, 1f, .85f);

    private bool _debug = true;

    private void Start() 
    {
        _audioPlayer.clip = _track;
        _image = GetComponentInChildren<Image>();
        _tmp = GetComponentInChildren<TextMeshPro>();
        
        if (_debug) { Debug.Log($"we found image ({_image != null}) and TextField({_tmp != null})"); }
        
        _tmp.color = _white60;
        _image.color = _white60;
    }

    private void OnEnable()
    {
        if (_debug) { Debug.Log($"Checking track N°{_trackNum} returned {Collection.Check(_trackNum)}"); }
        
        if (Collection.Check(_trackNum))
        {            
            // set colours
            _image.sprite = playSprite;
            _tmp.font = regular;
            _tmp.color = _white85;
            _image.color = _white85;
        }
        else { _image.sprite = playSprite; }
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlaySong()
    {      
        // check if Item has been aquired
        if (Collection.Check(_trackNum)) 
        {
            _audioPlayer.clip = _track;                    
            _audioPlayer.Play();
            _audioPlayer.GetComponent<AudioPlayer>().AudioAnimations(false);

            if (_debug) { Debug.Log($"Now playing: {_track.name}"); }
            
            // UI
            _tmp.font = bold;
            _tmp.color = Color.white;
            _image.color = Color.white;
            // await end
            StartCoroutine(WaitForSongEnd());
        }
    }

    /// <summary>
    /// restart background music
    /// </summary>
    public void OnSongOver()
    {
        if (_debug) { Debug.Log("Sample End"); }
        _audioPlayer.GetComponent<AudioPlayer>().AudioAnimations(true);

        // reset colours
        _tmp.font = regular;
        _tmp.color = _white85;
        _image.color = _white85;
    }    

    /// <summary>
    /// Couroutine awaits end of song, checks every .5s
    /// </summary>
    /// <returns></returns>
    private IEnumerator WaitForSongEnd()
    {
        if (_debug) { Debug.Log("Awaiting Sample End"); }        
        
        while (_audioPlayer.isPlaying)
        {
            yield return new WaitForSeconds(0.5f); // Wait for 500ms
        }

        OnSongOver();
    }

}
