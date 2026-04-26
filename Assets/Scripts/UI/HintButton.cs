using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HintButton : MonoBehaviour
{
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private TMP_Text hintText;

    private Button button;
    private bool isShown;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ToggleHint);

        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(ToggleHint);
        }
    }

    private void ToggleHint()
    {
        isShown = !isShown;

        if (hintPanel != null)
        {
            hintPanel.SetActive(isShown);
        }

        if (!isShown)
        {
            return;
        }

        if (hintText == null)
        {
            return;
        }

        if (LevelManager.Instance == null)
        {
            hintText.text = "LevelManager не найден.";
            return;
        }

        hintText.text = LevelManager.Instance.GetHintText();
    }
}