using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Teleport : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;
    [SerializeField] private float teleportCooldown = 0.2f;

    [Header("Velocity")]
    [SerializeField] private bool keepVelocity;
    [SerializeField] private Vector2 customExitVelocity;

    private static readonly Dictionary<Collider2D, float> blockedUntilByPlayer = new();

    private void Awake()
    {
        Collider2D doorCollider = GetComponent<Collider2D>();
        doorCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (exitPoint == null)
        {
            Debug.LogWarning($"{name}: exitPoint не назначен.");
            return;
        }

        if (IsBlockedByCooldown(other))
        {
            return;
        }

        TeleportPlayer(other);
    }

    private bool IsBlockedByCooldown(Collider2D playerCollider)
    {
        if (!blockedUntilByPlayer.TryGetValue(playerCollider, out float blockedUntil))
        {
            return false;
        }

        return Time.time < blockedUntil;
    }

    private void TeleportPlayer(Collider2D playerCollider)
    {
        Rigidbody2D playerRigidbody = playerCollider.attachedRigidbody;

        if (playerRigidbody != null)
        {
            playerRigidbody.position = exitPoint.position;

            if (!keepVelocity)
            {
                playerRigidbody.linearVelocity = customExitVelocity;
            }
        }
        else
        {
            playerCollider.transform.position = exitPoint.position;
        }

        blockedUntilByPlayer[playerCollider] = Time.time + teleportCooldown;
    }
}