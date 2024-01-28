using System.Collections;
using System.Collections.Generic;
using GameArchitecture;
using UnityEngine;

public class Barks : MonoBehaviour
{
    public AudioSource MusicSource;
    public AudioSource audioSource;
    public List<AudioClip> AudioClips;
    
    private float startVolume;
    private bool _isPlaying;
    private Coroutine _coroutine;
    
    IEnumerator TurnAudioOff(float time)
    {
        _isPlaying = true;
        startVolume = MusicSource.volume;
        MusicSource.volume = 0.02f;
        yield return new WaitForSeconds(time);
        _isPlaying = false;
        MusicSource.volume = startVolume;
    }
    
    // Get a random bark from the list and play it
    public void Bark()
    {
        AudioClip clip = AudioClips[Random.Range(0, AudioClips.Count)];
        if (_isPlaying)
        {
            StopCoroutine(_coroutine);
            MusicSource.volume = startVolume;
        }
        _coroutine = StartCoroutine(TurnAudioOff(clip.length)); 
        audioSource.PlayOneShot(AudioClips[Random.Range(0, AudioClips.Count)]);
    }
}
