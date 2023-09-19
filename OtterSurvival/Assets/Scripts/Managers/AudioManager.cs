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
    public void PlaySoundEffect(GameObject audioObject, AudioClip clip, float volume)
    {
        // Create an AudioSource for the sound effect
        AudioSource source = audioObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = mainVolume * sFXVolume * volume;
        source.Play();

        // Destroy the AudioSource after the sound has finished playing
        Destroy(source, clip.length);
    }

    // Play a sound effect when the same sound effect doesn't play
    public void PlaySoundEffectWhenSilent(AudioSource source, float volume, AudioClip clip = null)
    {
        volume = Mathf.Clamp(volume, 0f, 1f);
        // Checks if the audiosource is playing
        if (source.clip != null)
        {
            if (!source.isPlaying)
            {
                // Set the volume and play the sound effect
                source.volume = mainVolume * sFXVolume * volume;
                source.Play();
            }
        }
        else if (clip != null)
        {
            if (!source.isPlaying)
            {
                // Set the volume and play the sound effect
                source.clip = clip;
                source.volume = mainVolume * sFXVolume * volume;
                source.Play();
            }
        }
    }
}
