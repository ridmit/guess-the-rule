using UnityEngine;

public class PauseQuestTarget : MonoBehaviour
{
    [SerializeField] private int taskIndex;
    [SerializeField] private int taskCount = 3;

    private bool isArmed;
    private bool wasPaused;

    private void Start()
    {
        isArmed =
            PauseQuestState.IsQuestActive &&
            !PauseQuestState.IsTaskCompleted(taskIndex);

        wasPaused = PauseMenuController.IsPaused;
    }

    private void Update()
    {
        if (!isArmed)
        {
            return;
        }

        bool isPaused = PauseMenuController.IsPaused;

        if (isPaused && !wasPaused)
        {
            PauseQuestState.CompleteTask(taskIndex, taskCount);
            isArmed = false;
        }

        wasPaused = isPaused;
    }
}