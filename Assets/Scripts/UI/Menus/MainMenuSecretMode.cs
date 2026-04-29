using UnityEngine;

public class MainMenuSecretMode : MonoBehaviour
{
    [SerializeField] private GameObject secretSceneRoot;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        bool isSecretMode = SecretMenuState.ShouldEnterSecretMenu;

        if (secretSceneRoot != null)
        {
            secretSceneRoot.SetActive(isSecretMode);
        }

        if (!isSecretMode)
        {
            SecretMenuPlayerRuntime.DestroyInstance();
            return;
        }

        if (SecretMenuPlayerRuntime.Instance != null)
        {
            return;
        }

        if (playerPrefab == null || spawnPoint == null)
        {
            Debug.LogError("Secret menu player prefab or spawn point is not assigned.");
            return;
        }

        GameObject playerObject = Instantiate(
            playerPrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        if (!playerObject.TryGetComponent<SecretMenuPlayerRuntime>(out _))
        {
            playerObject.AddComponent<SecretMenuPlayerRuntime>();
        }
    }
}