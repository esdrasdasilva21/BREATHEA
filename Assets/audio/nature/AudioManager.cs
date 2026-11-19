
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource natureSource;
    public AudioSource uiSource;

    [Header("Background Music")]
    public AudioClip backgroundMusic;

    [Header("Nature Sounds")]
    public AudioClip natureSound;

    [Header("UI Sound Effects")]
    public AudioClip startSound;
    public AudioClip pauseSound;
    public AudioClip resumeSound;
    public AudioClip finishSound;

    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;

    [Range(0f, 1f)]
    public float natureVolume = 0.3f;

    [Range(0f, 1f)]
    public float uiVolume = 0.7f;

    private void Start()
    {
        ConfigureAudioSources();
        PlayBackgroundMusic();
        PlayNatureSounds();
    }

    private void ConfigureAudioSources()
    {
        if (musicSource != null)
        {
            musicSource.loop = true;
            musicSource.volume = musicVolume;
        }

        if (natureSource != null)
        {
            natureSource.loop = true;
            natureSource.volume = natureVolume;
        }

        if (uiSource != null)
        {
            uiSource.loop = false;
            uiSource.volume = uiVolume;
        }
    }

    public void PlayBackgroundMusic()
    {
        if (musicSource == null || backgroundMusic == null)
            return;

        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void PlayNatureSounds()
    {
        if (natureSource == null || natureSound == null)
            return;

        natureSource.clip = natureSound;
        natureSource.Play();
    }

    public void PlayStartSound()
    {
        PlayUISound(startSound);
    }

    public void PlayPauseSound()
    {
        PlayUISound(pauseSound);
    }

    public void PlayResumeSound()
    {
        PlayUISound(resumeSound);
    }

    public void PlayFinishSound()
    {
        PlayUISound(finishSound);
    }

    private void PlayUISound(AudioClip clip)
    {
        if (uiSource == null || clip == null)
            return;

        uiSource.PlayOneShot(clip);
    }

    public void StopAllAudio()
    {
        if (musicSource != null)
            musicSource.Stop();

        if (natureSource != null)
            natureSource.Stop();

        if (uiSource != null)
            uiSource.Stop();
    }
}
