using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField, Tooltip("Sound that plays when you pick something up")]
    private AudioClip pickUp = null;
    [SerializeField, Tooltip("Sound that plays when you use something")]
    private AudioClip use = null;
    [SerializeField, Tooltip("Sound that plays when you take damage")]
    private AudioClip damage = null;
    [SerializeField, Tooltip("Sound that plays when you die")]
    private AudioClip death = null;
    [SerializeField, Tooltip("Sound that plays when you are swimming")]
    private AudioClip swimming = null;
    [SerializeField, Tooltip("Music that plays in the background")]
    private AudioClip music = null;

    [Header("Settings")]
    [SerializeField, Tooltip("Where the music is going to be played")]
    private AudioSource musicPlayer = null;

    [SerializeField, Tooltip("The volume of the entire sound system from 0 to 1"), Range(0f,1f)]
    private float mainVolume = 0.5f;
    [SerializeField, Tooltip("The volume of the sound effects from 0 to 1"), Range(0f, 1f)]
    private float sFXVolume = 1.0f;
    [SerializeField, Tooltip("The volume of the music from 0 to 1"), Range(0f, 1f)]
    private float musicVolume = 1.0f;

    // Play music
    private void Start()
    {
        if (musicPlayer != null && music != null)
        {
            musicPlayer.clip = music;
            musicPlayer.volume = mainVolume * musicVolume;
            musicPlayer.Play();
        }
    }

    // Play a pick up sound effect
    public void PlayPickUpSoundEffect(GameObject audioObject)
    {
        if (pickUp != null)
        {
            // Create an AudioSource for the sound effect
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.clip = pickUp;
            source.volume = mainVolume * sFXVolume;
            source.Play();

            // Destroy the AudioSource after the sound has finished playing
            Destroy(source, pickUp.length);
        }
    }

    // Play a use sound effect
    public void PlayUseSoundEffect(GameObject audioObject)
    {
        if (use != null)
        {
            // Create an AudioSource for the sound effect
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.clip = use;
            source.volume = mainVolume * sFXVolume;
            source.Play();

            // Destroy the AudioSource after the sound has finished playing
            Destroy(source, use.length);
        }
    }

    // Play a death sound effect
    public void PlayDeathSoundEffect(GameObject audioObject)
    {
        if (death != null)
        {
            // Create an AudioSource for the sound effect
            AudioSource source = audioObject.AddComponent<AudioSource>();
            source.clip = death;
            source.volume = mainVolume * sFXVolume;
            source.Play();

            // Destroy the AudioSource after the sound has finished playing
            Destroy(source, death.length);
        }
    }

    // Play a damage sound effect
    public void PlayDamageSoundEffect(GameObject audioObject, AudioSource source)
    {
        if (damage != null && !source.isPlaying)
        {
            // Create an AudioSource for the sound effect
            source.clip = damage;
            source.volume = mainVolume * sFXVolume;
            source.Play();
        }
    }

    // Play a swimming sound effect
    public void PlaySwimmingSoundEffect(GameObject audioObject, AudioSource source)
    {
        // Create an AudioSource for the sound effect
        if (swimming != null && !source.isPlaying)
        {
            source.clip = swimming;
            source.volume = mainVolume * sFXVolume;
            source.Play();
        }
    }
}
