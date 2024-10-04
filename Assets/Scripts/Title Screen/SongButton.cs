using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongButton : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    private AudioPlayer _audioPlayer;
    [Space]
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextMeshProUGUI _tmp;
    public Sprite lockSprite;
    public Sprite playSprite;
    public TMP_FontAsset regular;
    public TMP_FontAsset bold;
    [Space]
    [SerializeField]
    private int _trackNum = 0;
    [SerializeField]
    private AudioClip _track;

    private Color _white60 = new Color(1f, 1f, 1f, .6f);
    private Color _white85 = new Color(1f, 1f, 1f, .85f);

    private bool _debug = true;

    private void Start() 
    {
        _audioPlayer = _audioSource.GetComponent<AudioPlayer>();

        _tmp.color = _white60;
        _image.color = _white60;
    }

    private void OnEnable()
    {
        // if (_debug) { Debug.Log($"Checking track N°{_trackNum} returned {Check()}"); }
        if (Check())
        {            
            // set colours
            _image.sprite = playSprite;
            _tmp.font = regular;
            _tmp.color = _white85;
            _image.color = _white85;
        }
        else 
        { 
            _image.sprite = lockSprite;
            _tmp.font = regular;
            _tmp.color = _white60;
            _image.color = _white60;
        }
    }

    /// <summary>
    /// check if track is unlocked in Collection
    /// </summary>
    /// <returns></returns>
    private bool Check() => Collection.Check(_trackNum);

    /// <summary>
    /// 
    /// </summary>
    public void PlaySong()
    {      
        // check if Item has been aquired
        if (Collection.Check(_trackNum)) 
        {
            _audioSource.clip = _track;                    
            _audioSource.Play();

            _audioPlayer.AudioAnimations(false);
            _audioPlayer.OnLoopEnd += OnSongOver;

            if (_debug) { Debug.Log($"Now playing: {_trackNum} : {_track.name}"); }
            
            // UI
            _tmp.font = bold;
            _tmp.color = Color.white;
            _image.color = Color.white;
        }    
    }
     

    /// <summary>
    /// restart background music
    /// </summary>
    public void OnSongOver()
    {
        if (_debug) { Debug.Log("Sample End"); }
        _audioPlayer.OnLoopEnd -= OnSongOver;

        // UI
        _tmp.font = regular;
        _tmp.color = _white85;
        _image.color = _white85;
    }

}
