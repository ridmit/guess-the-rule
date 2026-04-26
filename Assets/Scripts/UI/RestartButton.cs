using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RestartButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(RestartLevel);
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
}