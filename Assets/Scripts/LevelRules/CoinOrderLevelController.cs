using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinOrderLevelController : MonoBehaviour
{
    [Header("Order")]
    [SerializeField] private int[] correctOrder = { 0, 1, 2 };

    [Header("Barrel")]
    [SerializeField] private Animator barrelAnimator;
    [SerializeField] private Collider2D barrelCollider;
    [SerializeField] private GameObject barrelObject;
    [SerializeField] private string breakTriggerName = "break";
    [SerializeField] private float barrelDisableDelay = 0.6f;

    private int currentOrderIndex;
    private bool isCompleted;
    private bool isRestarting;

    public void TryCollectCoin(OrderedCoin coin)
    {
        if (isCompleted || isRestarting)
        {
            return;
        }

        if (coin == null)
        {
            return;
        }

        if (currentOrderIndex >= correctOrder.Length)
        {
            return;
        }

        int expectedCoinId = correctOrder[currentOrderIndex];

        if (coin.CoinId != expectedCoinId)
        {
            RestartLevel();
            return;
        }

        coin.MarkCollected();
        currentOrderIndex++;

        if (currentOrderIndex >= correctOrder.Length)
        {
            CompleteLevelRule();
        }
    }

    private void CompleteLevelRule()
    {
        isCompleted = true;

        if (barrelAnimator != null)
        {
            barrelAnimator.SetTrigger(breakTriggerName);
        }

        if (barrelCollider != null)
        {
            barrelCollider.enabled = false;
        }

        StartCoroutine(DisableBarrelAfterDelay());
    }

    private IEnumerator DisableBarrelAfterDelay()
    {
        yield return new WaitForSeconds(barrelDisableDelay);

        if (barrelObject != null)
        {
            barrelObject.SetActive(false);
        }
    }

    private void RestartLevel()
    {
        if (isRestarting)
        {
            return;
        }

        isRestarting = true;
        Time.timeScale = 1f;

        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.RestartLevel();
            return;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}