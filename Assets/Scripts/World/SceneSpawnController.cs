using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawnController : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return null;

        if (!SceneSpawnState.HasPendingSpawn)
        {
            yield break;
        }

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (SceneSpawnState.TargetSceneName != currentSceneName)
        {
            yield break;
        }

        SceneSpawnPoint spawnPoint = FindSpawnPoint(SceneSpawnState.SpawnPointId);
        Player player = FindFirstObjectByType<Player>();

        if (spawnPoint == null)
        {
            Debug.LogWarning($"Spawn point not found: {SceneSpawnState.SpawnPointId}");
            SceneSpawnState.Clear();
            yield break;
        }

        if (player == null)
        {
            Debug.LogWarning("Player not found on spawned scene.");
            SceneSpawnState.Clear();
            yield break;
        }

        MovePlayer(player, spawnPoint.Position);
        player.ApplyExternalHorizontalInput(
            SceneSpawnState.ForcedHorizontalInput,
            SceneSpawnState.ForcedInputDuration
        );

        SceneSpawnState.Clear();
    }

    private SceneSpawnPoint FindSpawnPoint(string spawnPointId)
    {
        SceneSpawnPoint[] spawnPoints = FindObjectsByType<SceneSpawnPoint>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (SceneSpawnPoint spawnPoint in spawnPoints)
        {
            if (spawnPoint.SpawnPointId == spawnPointId)
            {
                return spawnPoint;
            }
        }

        return null;
    }

    private void MovePlayer(Player player, Vector3 position)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.position = position;
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            return;
        }

        player.transform.position = position;
    }
}