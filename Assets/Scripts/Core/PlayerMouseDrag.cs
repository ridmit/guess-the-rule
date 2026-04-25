using UnityEngine;

public class PlayerMouseDrag : MonoBehaviour
{
    [SerializeField] private MonoBehaviour playerMovementScript;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float skinWidth = 0.03f;

    private Camera mainCamera;
    private Rigidbody2D playerRigidbody;
    private Collider2D playerCollider;

    private float defaultGravityScale;
    private Vector3 dragOffset;
    private bool isDragging;

    private readonly RaycastHit2D[] castResults = new RaycastHit2D[8];

    private void Awake()
    {
        mainCamera = Camera.main;
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (playerRigidbody != null)
        {
            defaultGravityScale = playerRigidbody.gravityScale;
            playerRigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        }
    }

    private void OnMouseDown()
    {
        if (mainCamera == null || playerRigidbody == null || playerCollider == null)
        {
            return;
        }

        isDragging = true;

        if (animator != null)
        {
            animator.SetBool("drag", true);
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        playerRigidbody.linearVelocity = Vector2.zero;
        playerRigidbody.gravityScale = 0f;

        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        dragOffset = transform.position - mouseWorldPosition;
    }

    private void FixedUpdate()
    {
        if (!isDragging || playerRigidbody == null || playerCollider == null)
        {
            return;
        }

        Vector3 mouseWorldPosition = GetMouseWorldPosition();
        Vector2 targetPosition = mouseWorldPosition + dragOffset;
        Vector2 currentPosition = playerRigidbody.position;
        Vector2 movement = targetPosition - currentPosition;

        if (movement.sqrMagnitude <= 0.0001f)
        {
            return;
        }

        Vector2 safeMovement = GetSafeMovement(movement);
        playerRigidbody.MovePosition(currentPosition + safeMovement);
    }

    private Vector2 GetSafeMovement(Vector2 movement)
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(obstacleLayer);
        contactFilter.useTriggers = false;

        float distance = movement.magnitude;
        Vector2 direction = movement.normalized;

        int hitCount = playerCollider.Cast(direction, contactFilter, castResults, distance + skinWidth);

        if (hitCount == 0)
        {
            return movement;
        }

        float closestDistance = distance;

        for (int i = 0; i < hitCount; i++)
        {
            if (castResults[i].distance < closestDistance)
            {
                closestDistance = castResults[i].distance;
            }
        }

        float safeDistance = Mathf.Max(0f, closestDistance - skinWidth);
        return direction * safeDistance;
    }

    private void OnMouseUp()
    {
        StopDragging();
    }

    private void OnDisable()
    {
        StopDragging();
    }

    private void StopDragging()
    {
        if (!isDragging)
        {
            return;
        }

        isDragging = false;

        if (animator != null)
        {
            animator.SetBool("drag", false);
        }

        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }

        if (playerRigidbody != null)
        {
            playerRigidbody.gravityScale = defaultGravityScale;
            playerRigidbody.linearVelocity = Vector2.zero;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -mainCamera.transform.position.z;

        return mainCamera.ScreenToWorldPoint(mousePosition);
    }
}