
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

    private void Awake()
    {
        LoadVolumeSettings();
    }

    private void Start()
    {
        ConfigureAudioSources();
        PlayBackgroundMusic();
        PlayNatureSounds();
    }

    private void LoadVolumeSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        natureVolume = PlayerPrefs.GetFloat("NatureVolume", natureVolume);
        uiVolume = PlayerPrefs.GetFloat("UIVolume", uiVolume);
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("NatureVolume", natureVolume);
        PlayerPrefs.SetFloat("UIVolume", uiVolume);

        PlayerPrefs.Save();
    }

    private void ConfigureAudioSources()
    {
        SetMusicVolume(musicVolume);
        SetNatureVolume(natureVolume);
        SetEffectsVolume(uiVolume);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);

        if (musicSource != null)
            musicSource.volume = musicVolume;

        SaveVolumeSettings();
    }

    public void SetNatureVolume(float value)
    {
        natureVolume = Mathf.Clamp01(value);

        if (natureSource != null)
            natureSource.volume = natureVolume;

        SaveVolumeSettings();
    }

    public void SetEffectsVolume(float value)
    {
        uiVolume = Mathf.Clamp01(value);

        if (uiSource != null)
            uiSource.volume = uiVolume;

        SaveVolumeSettings();
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

    private void Awake()
    {
        LoadVolumeSettings();
    }

    private void Start()
    {
        ConfigureAudioSources();
        PlayBackgroundMusic();
        PlayNatureSounds();
    }

    private void LoadVolumeSettings()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", musicVolume);
        natureVolume = PlayerPrefs.GetFloat("NatureVolume", natureVolume);
        uiVolume = PlayerPrefs.GetFloat("UIVolume", uiVolume);
    }

    private void SaveVolumeSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("NatureVolume", natureVolume);
        PlayerPrefs.SetFloat("UIVolume", uiVolume);

        PlayerPrefs.Save();
    }

    private void ConfigureAudioSources()
    {
        SetMusicVolume(musicVolume);
        SetNatureVolume(natureVolume);
        SetEffectsVolume(uiVolume);
    }

    public void SetMusicVolume(float value)
    {
        musicVolume = Mathf.Clamp01(value);

        if (musicSource != null)
            musicSource.volume = musicVolume;

        SaveVolumeSettings();
    }

    public void SetNatureVolume(float value)
    {
        natureVolume = Mathf.Clamp01(value);

        if (natureSource != null)
            natureSource.volume = natureVolume;

        SaveVolumeSettings();
    }

    public void SetEffectsVolume(float value)
    {
        uiVolume = Mathf.Clamp01(value);

        if (uiSource != null)
            uiSource.volume = uiVolume;

        SaveVolumeSettings();
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
