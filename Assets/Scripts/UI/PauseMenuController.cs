using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;

    [Header("Gameplay Root")]
    [SerializeField] private Transform gameplayRoot;

    [Header("Pause Visible Objects")]
    [SerializeField] private PauseVisibleObject[] pauseVisibleObjects;

    private MonoBehaviour[] gameplayScripts;
    private bool isPaused;
    private bool hasShownPauseVisibleObjects;

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

        SetPauseVisibleObjectsVisible(false);
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

        SetPauseVisibleObjectsVisible(false);
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

        if (!hasShownPauseVisibleObjects)
        {
            SetPauseVisibleObjectsVisible(true);
            hasShownPauseVisibleObjects = true;
        }

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
        SetPauseVisibleObjectsVisible(false);
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

    private void SetPauseVisibleObjectsVisible(bool visible)
    {
        foreach (PauseVisibleObject pauseVisibleObject in pauseVisibleObjects)
        {
            if (pauseVisibleObject != null)
            {
                pauseVisibleObject.SetVisible(visible);
            }
        }
    }

    private void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}