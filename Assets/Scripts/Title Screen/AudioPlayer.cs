using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> trackList = new List<AudioClip>();

    [Space]
    [Tooltip("speed of directional panning effect [left-right]")]
    public float panSpeed = 0.3f;
    [Tooltip("amount of directional panning effect [left-right]")]
    public float panAmount = 0.75f;
    [Tooltip("average amount of reverb")]
    public float avReverb = .9f;
    [Tooltip("max Volume")]
    public float maxVolume = .7f;

    public event Action OnLoopEnd;
    private Coroutine _audioAnimCoroutine;

    private AudioSource _player;
    private int _track = 0;
    
    private void Awake()
    {
        _player = GetComponent<AudioSource>();

        // handle song changes
        StartCoroutine(CheckPlayer());
        // handle audio animations
        AudioAnimations(true);
    }

    /// <summary>
    /// play songs
    /// </summary>
    private void PlaySong()
    {
        // play next
        _track = (_track + 1) % trackList.Count;
        _player.clip = trackList[_track];
        _player.Play();
    }

    /// <summary>
    /// handle song changes
    /// </summary>
    /// <returns></returns>
    private IEnumerator CheckPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f); // Wait for 500ms
            if (!_player.isPlaying)
            {
                OnLoopEnd?.Invoke();
                PlaySong();
            }
        }
    }

    /// <summary>
    /// set AudioAnimations true/ false
    /// </summary>
    /// <param name="t"></param>
    public void AudioAnimations(bool t)
    {
        if (t && _audioAnimCoroutine == null)
        {
            _audioAnimCoroutine = StartCoroutine(AudioAnimationCoroutine());
        }
        else if (_audioAnimCoroutine != null)
        {
            StopCoroutine(_audioAnimCoroutine);
            _audioAnimCoroutine = null;
        }
        Debug.Log("AudioAnimations: " + t);
    }

    /// <summary>
    /// handle song animations
    /// </summary>
    /// <returns></returns>
    private IEnumerator AudioAnimationCoroutine()
    {
        while (true) // Run indefinitely
        {
            // Calculate the pan value using a sine wave
            float panValue = Mathf.Sin(Time.time * panSpeed) * panAmount;
            // Calculate reverb values
            float reverb = avReverb * .75f + Mathf.Cos(Time.time * panSpeed) * avReverb / 2;
            reverb = Mathf.Clamp(reverb, 0, 1.25f);
            // calculate volumes
            maxVolume = Mathf.Clamp(maxVolume, 0, 1);
            float minVolume = maxVolume * .6f;
            float volume = minVolume - Mathf.Cos(Time.time * panSpeed) * (maxVolume - minVolume);

            // pass values
            _player.panStereo = panValue;
            _player.reverbZoneMix = reverb;
            _player.volume = volume;

            // Wait for the next frame
            yield return null;
        }
    }

    

}
