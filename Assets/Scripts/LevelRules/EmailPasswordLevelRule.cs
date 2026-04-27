using System;
using UnityEngine;

public class EmailPasswordLevelRule : MonoBehaviour
{
    [SerializeField] private PasswordDigitButton[] digitButtons;

    [Header("Barrel")]
    [SerializeField] private GameObject barrelBlocker;
    [SerializeField] private Animator barrelAnimator;
    [SerializeField] private Collider2D barrelCollider;
    [SerializeField] private float hideBarrelDelay = 0.6f;

    private bool isSolved;

    private void Awake()
    {
        foreach (PasswordDigitButton digitButton in digitButtons)
        {
            if (digitButton != null)
            {
                digitButton.DigitChanged += CheckPassword;
            }
        }
    }

    private void Start()
    {
        CheckPassword();
    }

    private void OnDestroy()
    {
        foreach (PasswordDigitButton digitButton in digitButtons)
        {
            if (digitButton != null)
            {
                digitButton.DigitChanged -= CheckPassword;
            }
        }
    }

    private void CheckPassword()
    {
        if (isSolved)
        {
            return;
        }

        string enteredPassword = GetEnteredPassword();

        if (!IsPasswordValid(enteredPassword))
        {
            return;
        }

        isSolved = true;
        BreakBarrel();
    }

    private string GetEnteredPassword()
    {
        string result = "";

        foreach (PasswordDigitButton digitButton in digitButtons)
        {
            if (digitButton != null)
            {
                result += digitButton.Value.ToString();
            }
        }

        return result;
    }

    private bool IsPasswordValid(string enteredPassword)
    {
        DateTime utcNow = DateTime.UtcNow;
        return enteredPassword == GeneratePassword(utcNow);
    }

    private string GeneratePassword(DateTime time)
    {
        int year = time.Year;
        int month = time.Month;
        int day = time.Day;
        int hour = time.Hour;
        int fiveMinuteBlock = time.Minute / 5;

        int rawValue =
            year * 13 +
            month * 17 +
            day * 19 +
            hour * 23 +
            fiveMinuteBlock * 29;

        int code = rawValue % 100000;

        return code.ToString("D5");
    }

    private void BreakBarrel()
    {
        if (barrelCollider != null)
        {
            barrelCollider.enabled = false;
        }

        if (barrelAnimator != null)
        {
            barrelAnimator.SetTrigger("break");
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