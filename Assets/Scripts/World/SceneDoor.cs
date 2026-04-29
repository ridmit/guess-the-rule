using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class SceneDoor : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private bool useRememberedReturn;
    [SerializeField] private string targetSceneName;
    [SerializeField] private string spawnPointId;

    [Header("Secret Menu On Arrival")]
    [SerializeField] private bool enableSecretMenuOnArrival;

    [Header("Exit Movement")]
    [SerializeField] private float forcedHorizontalInput;
    [SerializeField] private float forcedInputDuration = 0.25f;

    [Header("Remember Return")]
    [SerializeField] private bool rememberReturnForNextScene;
    [SerializeField] private string returnSceneName;
    [SerializeField] private string returnSpawnPointId;
    [SerializeField] private bool enableSecretMenuOnReturn = true;
    [SerializeField] private float returnForcedHorizontalInput = -1f;
    [SerializeField] private float returnForcedInputDuration = 0.25f;

    [Header("Cooldown")]
    [SerializeField] private float transitionCooldown = 0.5f;

    private static float blockedUntilTime;

    private void Awake()
    {
        Collider2D doorCollider = GetComponent<Collider2D>();
        doorCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Time.unscaledTime < blockedUntilTime)
        {
            return;
        }

        if (!other.TryGetComponent<Player>(out _))
        {
            return;
        }

        if (useRememberedReturn)
        {
            LoadRememberedReturn();
            return;
        }

        LoadFixedTarget();
    }

    private void LoadFixedTarget()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogWarning($"{name}: targetSceneName не назначен.");
            return;
        }

        if (string.IsNullOrEmpty(spawnPointId))
        {
            Debug.LogWarning($"{name}: spawnPointId не назначен.");
            return;
        }

        if (rememberReturnForNextScene)
        {
            if (string.IsNullOrEmpty(returnSceneName))
            {
                Debug.LogWarning($"{name}: returnSceneName не назначен.");
                return;
            }

            if (string.IsNullOrEmpty(returnSpawnPointId))
            {
                Debug.LogWarning($"{name}: returnSpawnPointId не назначен.");
                return;
            }

            SceneReturnState.SetReturn(
                returnSceneName,
                returnSpawnPointId,
                returnForcedHorizontalInput,
                returnForcedInputDuration,
                enableSecretMenuOnReturn
            );
        }

        ApplySecretMenuState(enableSecretMenuOnArrival);

        SceneSpawnState.SetPendingSpawn(
            targetSceneName,
            spawnPointId,
            forcedHorizontalInput,
            forcedInputDuration
        );

        LoadScene(targetSceneName);
    }

    private void LoadRememberedReturn()
    {
        if (!SceneReturnState.HasReturn)
        {
            Debug.LogWarning($"{name}: remembered return не найден.");
            return;
        }

        string rememberedSceneName = SceneReturnState.ReturnSceneName;
        string rememberedSpawnPointId = SceneReturnState.ReturnSpawnPointId;
        float rememberedForcedInput = SceneReturnState.ReturnForcedHorizontalInput;
        float rememberedForcedDuration = SceneReturnState.ReturnForcedInputDuration;
        bool enableSecretMenu = SceneReturnState.EnableSecretMenuOnReturn;

        SceneReturnState.Clear();

        ApplySecretMenuState(enableSecretMenu);

        SceneSpawnState.SetPendingSpawn(
            rememberedSceneName,
            rememberedSpawnPointId,
            rememberedForcedInput,
            rememberedForcedDuration
        );

        LoadScene(rememberedSceneName);
    }

    private void ApplySecretMenuState(bool enableSecretMenu)
    {
        if (enableSecretMenu)
        {
            SecretMenuState.EnterSecretMenu();
            return;
        }

        SecretMenuState.ExitSecretMenu();
        SecretMenuPlayerRuntime.DestroyInstance();
    }

    private void LoadScene(string sceneName)
    {
        blockedUntilTime = Time.unscaledTime + transitionCooldown;

        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}