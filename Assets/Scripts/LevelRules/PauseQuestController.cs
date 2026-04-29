using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseQuestController : MonoBehaviour
{
    private const string PauseText = "II";
    private const string CompletedText = "Х";

    [Serializable]
    private class QuestIcon
    {
        public int taskIndex;
        public Image image;
        public TMP_Text label;
        public string levelText;
    }

    [Header("Quest")]
    [SerializeField] private int taskCount = 3;
    [SerializeField] private float switchInterval = 1f;

    [Header("Sprites")]
    [SerializeField] private Sprite levelButtonSprite;

    [Header("Objects")]
    [SerializeField] private GameObject barrelBlocker;

    [Header("Icons")]
    [SerializeField] private QuestIcon[] questIcons;

    private float timer;
    private bool showPauseText;

    private void Start()
    {
        PauseQuestState.StartQuestIfNeeded(taskCount);
        RefreshState();
    }

    private void Update()
    {
        if (switchInterval <= 0f)
        {
            return;
        }

        timer += Time.deltaTime;

        if (timer < switchInterval)
        {
            return;
        }

        timer = 0f;
        showPauseText = !showPauseText;
        RefreshState();
    }

    private void RefreshState()
    {
        bool allCompleted = PauseQuestState.AreAllTasksCompleted(taskCount);

        if (barrelBlocker != null)
        {
            barrelBlocker.SetActive(!allCompleted);
        }

        foreach (QuestIcon questIcon in questIcons)
        {
            RefreshIcon(questIcon);
        }
    }

    private void RefreshIcon(QuestIcon questIcon)
    {
        if (questIcon == null || questIcon.image == null || questIcon.label == null)
        {
            return;
        }

        questIcon.image.sprite = levelButtonSprite;
        questIcon.label.gameObject.SetActive(true);

        if (PauseQuestState.IsTaskCompleted(questIcon.taskIndex))
        {
            questIcon.label.text = CompletedText;
            return;
        }

        questIcon.label.text = showPauseText ? PauseText : questIcon.levelText;
    }

    [ContextMenu("Reset Pause Quest")]
    private void ResetQuest()
    {
        PauseQuestState.ResetQuest(taskCount);
        RefreshState();
    }
}