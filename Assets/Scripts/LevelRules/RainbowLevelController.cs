using UnityEngine;

public class RainbowLevelController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RainbowDigitButton digit1;
    [SerializeField] private RainbowDigitButton digit2;
    [SerializeField] private RainbowDigitButton digit3;

    [Header("Correct Code")]
    [SerializeField] private int correct1 = 2;
    [SerializeField] private int correct2 = 5;
    [SerializeField] private int correct3 = 7;

    [Header("Barrel")]
    [SerializeField] private GameObject barrelBlocker;
    [SerializeField] private Animator barrelAnimator;
    [SerializeField] private Collider2D barrelCollider;
    [SerializeField] private string breakTriggerName = "break";
    [SerializeField] private float hideBarrelDelay = 0.6f;

    private bool isSolved;

    private void Awake()
    {
        if (digit1 != null)
        {
            digit1.ValueChanged += CheckCode;
        }

        if (digit2 != null)
        {
            digit2.ValueChanged += CheckCode;
        }

        if (digit3 != null)
        {
            digit3.ValueChanged += CheckCode;
        }
    }

    private void Start()
    {
        CheckCode();
    }

    private void OnDestroy()
    {
        if (digit1 != null)
        {
            digit1.ValueChanged -= CheckCode;
        }

        if (digit2 != null)
        {
            digit2.ValueChanged -= CheckCode;
        }

        if (digit3 != null)
        {
            digit3.ValueChanged -= CheckCode;
        }
    }

    private void CheckCode()
    {
        if (isSolved)
        {
            return;
        }

        if (digit1 == null || digit2 == null || digit3 == null)
        {
            return;
        }

        if (digit1.Value != correct1)
        {
            return;
        }

        if (digit2.Value != correct2)
        {
            return;
        }

        if (digit3.Value != correct3)
        {
            return;
        }

        SolveLevel();
    }

    private void SolveLevel()
    {
        isSolved = true;

        Debug.Log("Rainbow level solved.");

        digit1.FallDown();
        digit2.FallDown();
        digit3.FallDown();

        BreakBarrel();
    }

    private void BreakBarrel()
    {
        if (barrelCollider != null)
        {
            barrelCollider.enabled = false;
        }

        if (barrelAnimator != null)
        {
            barrelAnimator.SetTrigger(breakTriggerName);
            Invoke(nameof(HideBarrel), hideBarrelDelay);
            return;
        }

        HideBarrel();
    }

    private void HideBarrel()
    {
        if (barrelBlocker != null)
        {
            barrelBlocker.SetActive(false);
        }
    }
}