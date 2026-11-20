
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [Header("References")]
    public AudioManager audioManager;
    public GameObject settingsPanel;
    public GameObject selectionPanel;

    [Header("Sliders")]
    public Slider musicSlider;
    public Slider natureSlider;
    public Slider effectsSlider;

    [Header("Buttons")]
    public Button backButton;

    private void Start()
    {
        settingsPanel.SetActive(false);

        musicSlider.minValue = 0f;
        musicSlider.maxValue = 1f;

        natureSlider.minValue = 0f;
        natureSlider.maxValue = 1f;

        effectsSlider.minValue = 0f;
        effectsSlider.maxValue = 1f;

        musicSlider.value = audioManager.musicVolume;
        natureSlider.value = audioManager.natureVolume;
        effectsSlider.value = audioManager.uiVolume;

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        natureSlider.onValueChanged.AddListener(SetNatureVolume);
        effectsSlider.onValueChanged.AddListener(SetEffectsVolume);

        backButton.onClick.AddListener(CloseSettings);
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        selectionPanel.SetActive(false);
    }

    private void SetMusicVolume(float value)
    {
        audioManager.SetMusicVolume(value);
    }

    private void SetNatureVolume(float value)
    {
        audioManager.SetNatureVolume(value);
    }

    private void SetEffectsVolume(float value)
    {
        audioManager.SetEffectsVolume(value);
    }

    private void CloseSettings()
    {
        settingsPanel.SetActive(false);
        selectionPanel.SetActive(true);
    }
}
