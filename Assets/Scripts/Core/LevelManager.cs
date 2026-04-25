using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    [SerializeField] private Transform player;
    [SerializeField] private Transform startPoint;
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

        if (player != null)
        {
            player.gameObject.SetActive(false);
        }

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