using System.Collections;
using System.Text;
using UnityEngine;

public class QrTilemapWallLevelController : MonoBehaviour
{
    [System.Serializable]
    public class QrCodeEntry
    {
        public string id;
        public Sprite qrSprite;
        public string code;
    }

    [Header("QR")]
    [SerializeField] private SpriteRenderer qrDisplay;
    [SerializeField] private QrCodeEntry[] qrCodes;

    [Header("Input")]
    [SerializeField] private DigitCycleButton[] digitButtons;
    [SerializeField] private int startDigit = 0;

    [Header("Tilemap Wall")]
    [SerializeField] private Transform wallRoot;
    [SerializeField] private Collider2D wallCollider;
    [SerializeField] private Vector3 openOffset = new Vector3(0f, -4f, 0f);
    [SerializeField] private float openDuration = 0.8f;

    private string currentCode;
    private bool isUnlocked;
    private Vector3 wallClosedPosition;
    private Vector3 wallOpenedPosition;

    private void Awake()
    {
        foreach (DigitCycleButton digitButton in digitButtons)
        {
            if (digitButton != null)
            {
                digitButton.DigitChanged += CheckCode;
            }
        }
    }

    private void Start()
    {
        if (wallRoot != null)
        {
            wallClosedPosition = wallRoot.position;
            wallOpenedPosition = wallClosedPosition + openOffset;
        }

        SelectRandomQr();
        ResetButtons();
        CheckCode();
    }

    private void OnDestroy()
    {
        foreach (DigitCycleButton digitButton in digitButtons)
        {
            if (digitButton != null)
            {
                digitButton.DigitChanged -= CheckCode;
            }
        }
    }

    private void SelectRandomQr()
    {
        if (qrCodes == null || qrCodes.Length == 0)
        {
            Debug.LogError("QR codes are not assigned.");
            return;
        }

        int index = Random.Range(0, qrCodes.Length);
        QrCodeEntry selected = qrCodes[index];

        currentCode = selected.code;

        if (qrDisplay != null)
        {
            qrDisplay.sprite = selected.qrSprite;
        }

        if (digitButtons != null && currentCode.Length != digitButtons.Length)
        {
            Debug.LogWarning(
                $"Code length ({currentCode.Length}) does not match digit buttons count ({digitButtons.Length}).");
        }
    }

    private void ResetButtons()
    {
        if (digitButtons == null)
        {
            return;
        }

        foreach (DigitCycleButton digitButton in digitButtons)
        {
            if (digitButton != null)
            {
                digitButton.SetLocked(false);
                digitButton.SetValue(startDigit);
            }
        }
    }

    private void CheckCode()
    {
        if (isUnlocked)
        {
            return;
        }

        if (string.IsNullOrEmpty(currentCode))
        {
            return;
        }

        string enteredCode = GetEnteredCode();

        if (enteredCode == currentCode)
        {
            UnlockLevel();
        }
    }

    private string GetEnteredCode()
    {
        StringBuilder builder = new StringBuilder();

        foreach (DigitCycleButton digitButton in digitButtons)
        {
            if (digitButton != null)
            {
                builder.Append(digitButton.CurrentValue);
            }
        }

        return builder.ToString();
    }

    private void UnlockLevel()
    {
        if (isUnlocked)
        {
            return;
        }

        isUnlocked = true;

        foreach (DigitCycleButton digitButton in digitButtons)
        {
            if (digitButton != null)
            {
                digitButton.SetLocked(true);
            }
        }

        StartCoroutine(LowerWallRoutine());
    }

    private IEnumerator LowerWallRoutine()
    {
        if (wallRoot == null)
        {
            yield break;
        }

        Vector3 startPosition = wallRoot.position;
        float elapsed = 0f;

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / openDuration);

            wallRoot.position = Vector3.Lerp(startPosition, wallOpenedPosition, t);

            yield return null;
        }

    wallRoot.position = wallOpenedPosition;
    }
}