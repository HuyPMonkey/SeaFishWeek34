using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private Coroutine audioCoroutine;

    public AudioClip[] sounds;
    AudioSource _audioSource;
    void Awake()
    {

        _audioSource = GetComponent<AudioSource>();
    }

     void Start()
    {

    }

    public void Play (string name, float stopDelay)
    {
        _audioSource.PlayOneShot(Array.Find(sounds, s=>s.name == name));
        if (audioCoroutine != null)
            StopCoroutine(audioCoroutine);
        audioCoroutine = StartCoroutine(StopAudioAfterDelay(stopDelay));
    }



    private IEnumerator StopAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        _audioSource.Stop();
    }
}
