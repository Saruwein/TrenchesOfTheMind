using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioShenanigans : MonoBehaviour
{
    public AudioSource audioPlayer;

    public float panSpeed = 0.3f;   // Speed of the panning effect
    public float panAmount = 0.75f;  // Maximum pan amount (between -1 and 1)
    public float maxReverb = .9f;

    private void Start()
    {
        // Start the coroutine to animate the pan
        StartCoroutine(AudioAnimations());
    }

    private IEnumerator AudioAnimations()
    {
        while (true) // Run indefinitely
        {
            // Calculate the pan value using a sine wave
            float panValue = Mathf.Sin(Time.time * panSpeed) * panAmount;
            float reverb = maxReverb *.75f + Mathf.Cos(Time.time * panSpeed) * maxReverb/ 2;
            audioPlayer.panStereo = panValue;
            audioPlayer.reverbZoneMix = reverb;

            // Wait for the next frame
            yield return null;
        }
    }


    private void Update()
    {
        if (!audioPlayer.isPlaying)
        {
            StopCoroutine(AudioAnimations());
        }
    }

}
