using UnityEngine.SceneManagement;

public static class PauseQuestResetter
{
    private const string QuestSceneName = "Level10";
    private const int TaskCount = 3;

    public static void ResetIfCurrentSceneIsQuestScene()
    {
        if (SceneManager.GetActiveScene().name != QuestSceneName)
        {
            return;
        }

        PauseQuestState.ResetQuest(TaskCount);
    }
}