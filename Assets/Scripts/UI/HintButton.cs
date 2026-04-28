using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HintButton : MonoBehaviour, IPauseSensitive
{
    [SerializeField] private GameObject hintPanel;
    [SerializeField] private TMP_Text hintText;

    private Button button;
    private bool isShown;

    private void Awake()
    {
        EnsureReferences();

        if (hintPanel != null)
        {
            hintPanel.SetActive(false);
        }
    }

    private void OnEnable()
    {
        EnsureReferences();

        if (button != null)
        {
            button.onClick.AddListener(ToggleHint);
        }
    }

    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(ToggleHint);
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

    private void EnsureReferences()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
    }
}