using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class RainbowDigitButton : MonoBehaviour
{
    [SerializeField] private TMP_Text valueText;

    [Header("Value")]
    [SerializeField] private int minValue = 1;
    [SerializeField] private int maxValue = 7;
    [SerializeField] private int currentValue = 1;

    [Header("Fall")]
    [SerializeField] private float fallGravityScale = 3f;
    [SerializeField] private float randomTorque = 60f;

    private Rigidbody2D rb;
    private bool isFalling;

    public int Value => currentValue;

    public event Action ValueChanged;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.freezeRotation = true;

        RefreshView();
    }

    private void OnMouseDown()
    {
        if (isFalling)
        {
            return;
        }

        NextValue();
    }

    public void NextValue()
    {
        if (isFalling)
        {
            return;
        }

        currentValue++;

        if (currentValue > maxValue)
        {
            currentValue = minValue;
        }

        RefreshView();
        ValueChanged?.Invoke();
    }

    public void FallDown()
    {
        if (isFalling)
        {
            return;
        }

        isFalling = true;

        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallGravityScale;
        rb.freezeRotation = false;
        rb.AddTorque(UnityEngine.Random.Range(-randomTorque, randomTorque));
    }

    private void RefreshView()
    {
        if (valueText != null)
        {
            valueText.text = currentValue.ToString();
        }
    }
}