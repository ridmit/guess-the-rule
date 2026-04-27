using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
        }
    }

    private void OnDestroy()
    {
        if (exitButton != null)
        {
            exitButton.onClick.RemoveListener(ExitGame);
        }
    }

    private void ExitGame()
    {
        Debug.Log("Exit game clicked.");

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}