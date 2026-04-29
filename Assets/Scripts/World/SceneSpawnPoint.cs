using UnityEngine;

public class SceneSpawnPoint : MonoBehaviour
{
    [SerializeField] private string spawnPointId;

    public string SpawnPointId => spawnPointId;
    public Vector3 Position => transform.position;
}