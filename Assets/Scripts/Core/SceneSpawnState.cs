public static class SceneSpawnState
{
    public static string TargetSceneName { get; private set; }
    public static string SpawnPointId { get; private set; }
    public static float ForcedHorizontalInput { get; private set; }
    public static float ForcedInputDuration { get; private set; }

    public static bool HasPendingSpawn =>
        !string.IsNullOrEmpty(TargetSceneName) &&
        !string.IsNullOrEmpty(SpawnPointId);

    public static void SetPendingSpawn(
        string targetSceneName,
        string spawnPointId,
        float forcedHorizontalInput,
        float forcedInputDuration
    )
    {
        TargetSceneName = targetSceneName;
        SpawnPointId = spawnPointId;
        ForcedHorizontalInput = forcedHorizontalInput;
        ForcedInputDuration = forcedInputDuration;
    }

    public static void Clear()
    {
        TargetSceneName = null;
        SpawnPointId = null;
        ForcedHorizontalInput = 0f;
        ForcedInputDuration = 0f;
    }
}