using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;

    [Header("Gameplay Root")]
    [SerializeField] private Transform gameplayRoot;

    private MonoBehaviour[] gameplayScripts;
    private bool isPaused;

    private void Awake()
    {
        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (gameplayRoot != null)
        {
            gameplayScripts = gameplayRoot.GetComponentsInChildren<MonoBehaviour>(true);
        }

        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(OpenPauseMenu);
        }

        if (continueButton != null)
        {
            continueButton.onClick.AddListener(ClosePauseMenu);
        }

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(GoToMenu);
        }

        Time.timeScale = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ClosePauseMenu();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    private void OnDestroy()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveListener(OpenPauseMenu);
        }

        if (continueButton != null)
        {
            continueButton.onClick.RemoveListener(ClosePauseMenu);
        }

        if (menuButton != null)
        {
            menuButton.onClick.RemoveListener(GoToMenu);
        }

        Time.timeScale = 1f;
    }

    public void OpenPauseMenu()
    {
        if (pauseMenu == null)
        {
            return;
        }

        isPaused = true;
        pauseMenu.SetActive(true);
        SetGameplayScriptsEnabled(false);
        Time.timeScale = 0f;
    }

    public void ClosePauseMenu()
    {
        if (pauseMenu == null)
        {
            return;
        }

        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SetGameplayScriptsEnabled(true);
    }

    private void SetGameplayScriptsEnabled(bool enabled)
    {
        if (gameplayScripts == null)
        {
            return;
        }

        foreach (MonoBehaviour script in gameplayScripts)
        {
            if (script != null)
            {
                script.enabled = enabled;
            }
        }
    }

    private void GoToMenu()
    {
        Debug.Log("Menu button clicked. Main menu is not implemented yet.");
    }
}