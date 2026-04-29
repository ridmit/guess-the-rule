using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretMenuPlayerRuntime : MonoBehaviour
{
    private static readonly string[] AllowedSceneNames =
    {
        "MainMenu",
        "Settings",
        "LevelSelect"
    };

    public static SecretMenuPlayerRuntime Instance { get; private set; }

    private Rigidbody2D rb;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        rb = GetComponent<Rigidbody2D>();

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Instance = null;
        }
    }

    public void TeleportTo(Vector3 position)
    {
        transform.position = position;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    public static void DestroyInstance()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (IsAllowedScene(scene.name))
        {
            return;
        }

        SecretMenuState.ExitSecretMenu();
        Destroy(gameObject);
    }

    private bool IsAllowedScene(string sceneName)
    {
        foreach (string allowedSceneName in AllowedSceneNames)
        {
            if (sceneName == allowedSceneName)
            {
                return true;
            }
        }

        return false;
    }
}