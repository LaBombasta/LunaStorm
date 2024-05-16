using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [Header("--- Audio Source ---")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource SFXSource;

    [Header("--- Audio Clips ---")]
    public AudioClip Background;
    public AudioClip BossBattle;
    public AudioClip EnemyGunfire;
    public AudioClip PlayerGunfire;
    public AudioClip MissileSound;
    public AudioClip ShieldCreation;
    public AudioClip iveBeenHit;


    private void Start()
    {
        instance = this;
        musicSource.volume = 1f;
        musicSource.clip = Background;
        musicSource.Play();

    }

    public void PlaySoundEffects(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVol)
    {
        Debug.Log("Hi");
        if (!fadeIn)
        {
            double lengthOfSource = (double)source.clip.samples / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lengthOfSource - duration));
        }

        float time = 0f;
        float startVol = source.volume;

        while(time < duration)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVol, time / duration);
            yield return null;
        }

        yield break;
    }
    public void BossMusicTime()
    {
        StartCoroutine(StartBossBattle());
    }
    public IEnumerator StartBossBattle()
    {
        StartCoroutine(Fade(false, musicSource, 4, 0f));
        yield return new WaitForSeconds(1);
        musicSource.clip = BossBattle;
        musicSource.Play();
        musicSource.volume = 0f;
        StartCoroutine(Fade(true, musicSource, 3, 1f));


    }

}

// place  to call sound FX
//  audioManager.PlaySoundEffects(audioManager.xxxx);
