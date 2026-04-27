using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(OpenLevelSelect);
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

        if (exitButton != null)
        {
            exitButton.onClick.RemoveListener(ExitGame);
        }
    }

    private void OpenLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
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