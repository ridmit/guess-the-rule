public static class PauseQuestState
{
    private static bool isQuestStarted;
    private static bool isQuestCompleted;
    private static bool[] completedTasks;

    public static bool IsQuestStarted => isQuestStarted;

    public static bool IsQuestCompleted => isQuestCompleted;

    public static bool IsQuestActive => isQuestStarted && !isQuestCompleted;

    public static void StartQuestIfNeeded(int taskCount)
    {
        if (isQuestCompleted)
        {
            return;
        }

        if (isQuestStarted)
        {
            return;
        }

        completedTasks = new bool[taskCount];
        isQuestStarted = true;
        isQuestCompleted = false;
    }

    public static bool IsTaskCompleted(int taskIndex)
    {
        if (completedTasks == null)
        {
            return false;
        }

        if (taskIndex < 0 || taskIndex >= completedTasks.Length)
        {
            return false;
        }

        return completedTasks[taskIndex];
    }

    public static void CompleteTask(int taskIndex, int taskCount)
    {
        if (!IsQuestActive)
        {
            return;
        }

        if (completedTasks == null || completedTasks.Length != taskCount)
        {
            completedTasks = new bool[taskCount];
        }

        if (taskIndex < 0 || taskIndex >= completedTasks.Length)
        {
            return;
        }

        completedTasks[taskIndex] = true;

        if (AreAllTasksCompleted(taskCount))
        {
            isQuestStarted = false;
            isQuestCompleted = true;
        }
    }

    public static bool AreAllTasksCompleted(int taskCount)
    {
        if (completedTasks == null || completedTasks.Length != taskCount)
        {
            return false;
        }

        for (int i = 0; i < taskCount; i++)
        {
            if (!completedTasks[i])
            {
                return false;
            }
        }

        return true;
    }

    public static void ResetQuest(int taskCount)
    {
        completedTasks = new bool[taskCount];
        isQuestStarted = false;
        isQuestCompleted = false;
    }
}