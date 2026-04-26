using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint;
    [SerializeField] private LevelHintsConfig levelHintsConfig;
    [SerializeField] private float nextLevelDelay = 0.15f;

    private bool isLevelFinished;
    private bool isRestarting;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (levelHintsConfig == null)
        {
            levelHintsConfig = Resources.Load<LevelHintsConfig>("LevelHintsConfig");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    public void FinishLevel()
    {
        if (isLevelFinished || isRestarting)
        {
            return;
        }

        isLevelFinished = true;
        StartCoroutine(LoadNextLevelWithDelay());
    }

    public void KillPlayer()
    {
        if (isRestarting || isLevelFinished)
        {
            return;
        }

        isRestarting = true;
        RestartLevel();
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MovePlayerToStart()
    {
        if (player == null || startPoint == null)
        {
            return;
        }

        player.position = startPoint.position;
    }

    public string GetHintText()
    {
        int levelNumber = GetCurrentLevelNumber();

        if (levelHintsConfig == null)
        {
            return "Конфиг подсказок не найден.";
        }

        return levelHintsConfig.GetHint(levelNumber);
    }

    public string GetLevelTitle()
    {
        int levelNumber = GetCurrentLevelNumber();

        if (levelHintsConfig == null)
        {
            return "Уровень " + levelNumber;
        }

        return levelHintsConfig.LevelLabelText + " " + levelNumber;
    }

    private int GetCurrentLevelNumber()
    {
        return SceneManager.GetActiveScene().buildIndex + 1;
    }

    private IEnumerator LoadNextLevelWithDelay()
    {
        yield return new WaitForSeconds(nextLevelDelay);
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log("No next level in Build Settings.");
            return;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}