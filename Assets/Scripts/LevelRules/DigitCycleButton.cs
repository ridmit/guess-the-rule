using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class DigitCycleButton : MonoBehaviour
{
    [SerializeField] private TMP_Text digitText;
    [SerializeField] private int currentValue;

    private bool isLocked;

    public int CurrentValue => currentValue;

    public event Action DigitChanged;

    private void Awake()
    {
        RefreshView();
    }

    private void OnMouseDown()
    {
        if (isLocked)
        {
            return;
        }

        currentValue = (currentValue + 1) % 10;
        RefreshView();
        DigitChanged?.Invoke();
    }

    public void SetValue(int value)
    {
        currentValue = Mathf.Clamp(value, 0, 9);
        RefreshView();
        DigitChanged?.Invoke();
    }

    public void SetLocked(bool locked)
    {
        isLocked = locked;
    }

    private void RefreshView()
    {
        if (digitText != null)
        {
            digitText.text = currentValue.ToString();
        }
    }
}