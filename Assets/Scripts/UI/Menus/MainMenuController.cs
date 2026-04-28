using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(OpenLevelSelect);
        }

        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(OpenSettings);
        }

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    private void OnDestroy()
    {
        if (playButton != null)
        {
            playButton.onClick.RemoveListener(OpenLevelSelect);
        }

        if (settingsButton != null)
        {
            settingsButton.onClick.RemoveListener(OpenSettings);
        }

        if (exitButton != null)
        {
            exitButton.onClick.RemoveListener(ExitGame);
        }
    }

    private void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    private void OpenSettings()
    {
        SceneManager.LoadScene("Settings");
    }

    private void ExitGame()
    {
        Debug.Log("Exit game clicked.");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}