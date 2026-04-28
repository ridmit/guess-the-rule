using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CursorPlatformFollower : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    [SerializeField] private float maxMoveSpeed = 8f;

    private Rigidbody2D rb;
    private Vector2 targetPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        targetPosition = rb.position;
    }

    private void Update()
    {
        if (targetCamera == null)
        {
            return;
        }

        Vector3 mouseScreenPosition = Input.mousePosition;
        mouseScreenPosition.z = Mathf.Abs(targetCamera.transform.position.z);

        Vector3 worldPosition = targetCamera.ScreenToWorldPoint(mouseScreenPosition);
        targetPosition = new Vector2(worldPosition.x, worldPosition.y);
    }

    private void FixedUpdate()
    {
        Vector2 currentPosition = rb.position;

        Vector2 nextPosition = Vector2.MoveTowards(
            currentPosition,
            targetPosition,
            maxMoveSpeed * Time.fixedDeltaTime
        );

        rb.MovePosition(nextPosition);
    }
}