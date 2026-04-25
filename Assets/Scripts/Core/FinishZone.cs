using UnityEngine;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private GameObject finishVisual;

    private bool isCollected;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isCollected)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        isCollected = true;

        if (finishVisual != null)
        {
            finishVisual.SetActive(false);
        }

        Collider2D finishCollider = GetComponent<Collider2D>();
        if (finishCollider != null)
        {
            finishCollider.enabled = false;
        }

        LevelManager.Instance.FinishLevel();
    }
}