using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

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

    // Play a sound effect
    public void PlaySoundEffect(GameObject audioObject, AudioClip clip)
    {
        // Create an AudioSource for the sound effect
        AudioSource source = audioObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = mainVolume * sFXVolume;
        source.Play();

        // Destroy the AudioSource after the sound has finished playing
        Destroy(source, pickUp.length);
    }

    // Play a sound effect when the same sound effect doesn't play
    public void PlaySoundEffectWhenSilent(GameObject audioObject, AudioSource source)
    {
        // Checks if the audiosource is playing
        if (!source.isPlaying)
        {
            // Set the volume and play the sound effect
            source.volume = mainVolume * sFXVolume;
            source.Play();
        }
    }
}
