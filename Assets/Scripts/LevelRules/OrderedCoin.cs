using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class OrderedCoin : MonoBehaviour
{
    [SerializeField] private CoinOrderLevelController levelController;
    [SerializeField] private int coinId;

    private bool isCollected;

    public int CoinId => coinId;

    private void Awake()
    {
        Collider2D coinCollider = GetComponent<Collider2D>();
        coinCollider.isTrigger = true;
    }

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

        if (levelController == null)
        {
            Debug.LogWarning($"{name}: CoinOrderLevelController не назначен.");
            return;
        }

        levelController.TryCollectCoin(this);
    }

    public void MarkCollected()
    {
        isCollected = true;
        gameObject.SetActive(false);
    }
}