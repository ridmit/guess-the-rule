using System;
using TMPro;
using UnityEngine;

public class RainbowDigitButton : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;
    [SerializeField] private int currentValue = 1;

    public Action OnValueChanged;

    public int Value => currentValue;

    private void Start()
    {
        RefreshView();
    }

    public void NextValue()
    {
        currentValue = currentValue % 7 + 1;

        RefreshView();
        OnValueChanged?.Invoke();
    }

    public void SetValue(int value)
    {
        currentValue = Mathf.Clamp(value, 1, 7);
        RefreshView();
    }

    private void RefreshView()
    {
        if (valueText != null)
            valueText.text = currentValue.ToString();
    }
}