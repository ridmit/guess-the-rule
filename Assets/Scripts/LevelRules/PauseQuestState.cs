using UnityEngine;

public static class PauseQuestState
{
    private const string QuestStartedKey = "PauseQuestStarted";
    private const string QuestCompletedKey = "PauseQuestCompleted";
    private const string TaskKeyPrefix = "PauseQuestTask_";

    public static bool IsQuestStarted => PlayerPrefs.GetInt(QuestStartedKey, 0) == 1;

    public static bool IsQuestCompleted => PlayerPrefs.GetInt(QuestCompletedKey, 0) == 1;

    public static bool IsQuestActive => IsQuestStarted && !IsQuestCompleted;

    public static void StartQuestIfNeeded(int taskCount)
    {
        if (IsQuestCompleted)
        {
            return;
        }

        if (IsQuestStarted)
        {
            return;
        }

        ResetTasks(taskCount);

        PlayerPrefs.SetInt(QuestStartedKey, 1);
        PlayerPrefs.SetInt(QuestCompletedKey, 0);
        PlayerPrefs.Save();
    }

    public static bool IsTaskCompleted(int taskIndex)
    {
        return PlayerPrefs.GetInt(GetTaskKey(taskIndex), 0) == 1;
    }

    public static void CompleteTask(int taskIndex, int taskCount)
    {
        if (!IsQuestActive)
        {
            return;
        }

        if (IsTaskCompleted(taskIndex))
        {
            return;
        }

        PlayerPrefs.SetInt(GetTaskKey(taskIndex), 1);

        if (AreAllTasksCompleted(taskCount))
        {
            PlayerPrefs.SetInt(QuestStartedKey, 0);
            PlayerPrefs.SetInt(QuestCompletedKey, 1);
        }

        PlayerPrefs.Save();
    }

    public static bool AreAllTasksCompleted(int taskCount)
    {
        for (int i = 0; i < taskCount; i++)
        {
            if (!IsTaskCompleted(i))
            {
                return false;
            }
        }

        return true;
    }

    public static void ResetQuest(int taskCount)
    {
        PlayerPrefs.DeleteKey(QuestStartedKey);
        PlayerPrefs.DeleteKey(QuestCompletedKey);
        ResetTasks(taskCount);
        PlayerPrefs.Save();
    }

    private static void ResetTasks(int taskCount)
    {
        for (int i = 0; i < taskCount; i++)
        {
            PlayerPrefs.DeleteKey(GetTaskKey(i));
        }
    }

    private static string GetTaskKey(int taskIndex)
    {
        return $"{TaskKeyPrefix}{taskIndex}";
    }
}