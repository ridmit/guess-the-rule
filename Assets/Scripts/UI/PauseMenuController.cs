using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    public static bool IsPaused { get; private set; }

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button menuButton;

    [Header("Pause Visible Objects")]
    [SerializeField] private PauseVisibleObject[] pauseVisibleObjects;

    private readonly List<MonoBehaviour> disabledByPause = new();

    private bool isPaused;
    private bool hasShownPauseVisibleObjects;

    private void Awake()
    {
        IsPaused = false;
        isPaused = false;
        Time.timeScale = 1f;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
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

        EnablePauseSensitiveScripts();
        SetPauseVisibleObjectsVisible(false);

        IsPaused = false;
        Time.timeScale = 1f;
    }

    public void OpenPauseMenu()
    {
        if (pauseMenu == null)
        {
            return;
        }

        IsPaused = true;
        isPaused = true;

        pauseMenu.SetActive(true);

        if (!hasShownPauseVisibleObjects)
        {
            SetPauseVisibleObjectsVisible(true);
            hasShownPauseVisibleObjects = true;
        }

        DisablePauseSensitiveScripts();
        Time.timeScale = 0f;
    }

    public void ClosePauseMenu()
    {
        if (pauseMenu == null)
        {
            return;
        }

        IsPaused = false;
        isPaused = false;

        pauseMenu.SetActive(false);
        SetPauseVisibleObjectsVisible(false);

        Time.timeScale = 1f;
        EnablePauseSensitiveScripts();
    }

    private void DisablePauseSensitiveScripts()
    {
        disabledByPause.Clear();

        MonoBehaviour[] scripts = FindObjectsOfType<MonoBehaviour>(true);

        foreach (MonoBehaviour script in scripts)
        {
            if (script == null)
            {
                continue;
            }

            if (script is not IPauseSensitive)
            {
                continue;
            }

            if (!script.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (!script.enabled)
            {
                continue;
            }

            disabledByPause.Add(script);
            script.enabled = false;
        }
    }

    private void EnablePauseSensitiveScripts()
    {
        foreach (MonoBehaviour script in disabledByPause)
        {
            if (script != null)
            {
                script.enabled = true;
            }
        }

        disabledByPause.Clear();
    }

    private void SetPauseVisibleObjectsVisible(bool visible)
    {
        if (pauseVisibleObjects == null)
        {
            return;
        }

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
        IsPaused = false;
        Time.timeScale = 1f;
        EnablePauseSensitiveScripts();
        SceneManager.LoadScene("MainMenu");
    }
}