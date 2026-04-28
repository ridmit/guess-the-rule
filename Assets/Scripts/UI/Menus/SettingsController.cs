using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.minValue = 0f;
            musicVolumeSlider.maxValue = 1f;
            musicVolumeSlider.wholeNumbers = false;

            musicVolumeSlider.SetValueWithoutNotify(MusicManager.Volume);
            musicVolumeSlider.onValueChanged.AddListener(ChangeMusicVolume);
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(BackToMenu);
        }
    }

    private void OnDestroy()
    {
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
        }

        if (backButton != null)
        {
            backButton.onClick.RemoveListener(BackToMenu);
        }
    }

    private void ChangeMusicVolume(float volume)
    {
        MusicManager.SetVolume(volume);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}