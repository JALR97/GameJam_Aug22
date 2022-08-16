using System;
using System.Collections;
using UnityEngine;

public class MusicController : MonoBehaviour {
    public AudioSource thisAudioSource;
    public AudioClip[] songs;
    [Range(1f, 10f)] 
    public float introTime;
    private void Start() {
        thisAudioSource.clip = songs[0];
        thisAudioSource.Play();
    }
    public void Switch() {
        StartCoroutine(SwitchInternal());
    }
    public void Stop() {
        thisAudioSource.clip = songs[0];
        thisAudioSource.loop = false;
    }
    IEnumerator SwitchInternal() {
        thisAudioSource.loop = false;
        while (thisAudioSource.isPlaying) {
            yield return null;
        }
        thisAudioSource.clip = songs[1];
        thisAudioSource.loop = true;
        thisAudioSource.Play();
    }
}
