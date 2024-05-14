using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("--- Audio Source ---")]
    [SerializeField]
    AudioSource musicSource;
    [SerializeField]
    AudioSource SFXSource;

    [Header("--- Audio Clips ---")]
    public AudioClip Background;
    public AudioClip EnemyGunfire;
    public AudioClip PlayerGunfire;
    public AudioClip MissileSound;
    public AudioClip ShieldCreation;


    private void Start()
    {

        musicSource.clip = Background;
        musicSource.Play();

    }

    public void PlaySoundEffects(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}

// place  to call sound FX
//  audioManager.PlaySoundEffects(audioManager.xxxx);
