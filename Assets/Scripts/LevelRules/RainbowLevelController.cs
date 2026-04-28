using UnityEngine;

public class RainbowLevelController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private RainbowDigitButton digit1;
    [SerializeField] private RainbowDigitButton digit2;
    [SerializeField] private RainbowDigitButton digit3;

    [Header("Correct code")]
    [SerializeField] private int correct1 = 6;
    [SerializeField] private int correct2 = 2;
    [SerializeField] private int correct3 = 1;

    [Header("Barrel")]
    [SerializeField] private Animator barrelAnimator;
    [SerializeField] private string breakTriggerName = "break";

    private bool solved = false;

    private void Start()
    {
        if (digit1 != null) digit1.OnValueChanged += CheckCode;
        if (digit2 != null) digit2.OnValueChanged += CheckCode;
        if (digit3 != null) digit3.OnValueChanged += CheckCode;

        CheckCode();
    }

    public void CheckCode()
    {
        if (solved)
            return;

        if (digit1 == null || digit2 == null || digit3 == null)
            return;

        if (digit1.Value == correct1 &&
            digit2.Value == correct2 &&
            digit3.Value == correct3)
        {
            SolveLevel();
        }
    }

    private void SolveLevel()
    {
        solved = true;

        if (barrelAnimator != null)
            barrelAnimator.SetTrigger(breakTriggerName);
    }
}