using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class LevelTitleText : MonoBehaviour
{
    private TMP_Text titleText;

    private void Awake()
    {
        titleText = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        UpdateTitle();
    }

    private void UpdateTitle()
    {
        if (LevelManager.Instance == null)
        {
            titleText.text = "Уровень ?";
            return;
        }

        titleText.text = LevelManager.Instance.GetLevelTitle();
    }
}