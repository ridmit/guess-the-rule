using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour, IPauseSensitive
{
    private Button button;

    private void Awake()
    {
        EnsureReferences();
    }

    private void OnEnable()
    {
        EnsureReferences();

        if (button != null)
        {
            button.onClick.AddListener(RestartLevel);
        }
    }

    private void OnDisable()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(RestartLevel);
        }
    }

    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(RestartLevel);
        }
    }

    private void RestartLevel()
    {
        if (LevelManager.Instance == null)
        {
            Debug.LogWarning("LevelManager not found.");
            return;
        }

        LevelManager.Instance.RestartLevel();
    }

    private void EnsureReferences()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
    }
}