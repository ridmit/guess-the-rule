public static class SceneReturnState
{
    public static string ReturnSceneName { get; private set; }
    public static string ReturnSpawnPointId { get; private set; }
    public static float ReturnForcedHorizontalInput { get; private set; }
    public static float ReturnForcedInputDuration { get; private set; }
    public static bool EnableSecretMenuOnReturn { get; private set; }

    public static bool HasReturn =>
        !string.IsNullOrEmpty(ReturnSceneName) &&
        !string.IsNullOrEmpty(ReturnSpawnPointId);

    public static void SetReturn(
        string returnSceneName,
        string returnSpawnPointId,
        float returnForcedHorizontalInput,
        float returnForcedInputDuration,
        bool enableSecretMenuOnReturn
    )
    {
        ReturnSceneName = returnSceneName;
        ReturnSpawnPointId = returnSpawnPointId;
        ReturnForcedHorizontalInput = returnForcedHorizontalInput;
        ReturnForcedInputDuration = returnForcedInputDuration;
        EnableSecretMenuOnReturn = enableSecretMenuOnReturn;
    }

    public static void Clear()
    {
        ReturnSceneName = null;
        ReturnSpawnPointId = null;
        ReturnForcedHorizontalInput = 0f;
        ReturnForcedInputDuration = 0f;
        EnableSecretMenuOnReturn = false;
    }
}