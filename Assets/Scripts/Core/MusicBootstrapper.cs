using UnityEngine;

public static class MusicBootstrapper
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void CreateMusicManager()
    {
        if (MusicManager.Instance != null)
        {
            return;
        }

        MusicManager prefab = Resources.Load<MusicManager>("MusicManager");

        if (prefab == null)
        {
            Debug.LogError("MusicManager prefab not found in Assets/Resources/MusicManager.prefab");
            return;
        }

        Object.Instantiate(prefab);
    }
}