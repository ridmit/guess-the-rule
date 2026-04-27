using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectController : MonoBehaviour
{
    [SerializeField] private Transform levelButtonsContainer;
    [SerializeField] private Button levelButtonPrefab;
    [SerializeField] private Button backButton;

    [Header("Scene Settings")]
    [SerializeField] private int firstLevelBuildIndex = 2;

    private void Awake()
    {
        CreateLevelButtons();

        if (backButton != null)
        {
            backButton.onClick.AddListener(BackToMenu);
        }
    }

    private void OnDestroy()
    {
        if (backButton != null)
        {
            backButton.onClick.RemoveListener(BackToMenu);
        }
    }

    private void CreateLevelButtons()
    {
        if (levelButtonsContainer == null || levelButtonPrefab == null)
        {
            Debug.LogWarning("Level buttons container or prefab is not assigned.");
            return;
        }

        ClearOldButtons();

        int sceneCount = SceneManager.sceneCountInBuildSettings;

        for (int buildIndex = firstLevelBuildIndex; buildIndex < sceneCount; buildIndex++)
        {
            int levelNumber = buildIndex - firstLevelBuildIndex + 1;

            Button levelButton = Instantiate(levelButtonPrefab, levelButtonsContainer);
            levelButton.gameObject.SetActive(true);

            TMP_Text buttonText = levelButton.GetComponentInChildren<TMP_Text>();
            if (buttonText != null)
            {
                buttonText.text = levelNumber.ToString();
            }

            int capturedBuildIndex = buildIndex;
            levelButton.onClick.AddListener(() => LoadLevel(capturedBuildIndex));
        }
    }

    private void ClearOldButtons()
    {
        for (int i = levelButtonsContainer.childCount - 1; i >= 0; i--)
        {
            Destroy(levelButtonsContainer.GetChild(i).gameObject);
        }
    }

    private void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}