using UnityEngine;

public class Player : MonoBehaviour, IPauseSensitive
{
    public float speed;
    public float jumpForce;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;

    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        bool isGrounded = IsGrounded(out Rigidbody2D groundRigidbody);

        float groundVelocityX = 0f;

        if (isGrounded && groundRigidbody != null)
        {
            groundVelocityX = groundRigidbody.linearVelocity.x;
        }

        body.linearVelocityX = horizontalInput * speed + groundVelocityX;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        animator.SetBool("run", Mathf.Abs(horizontalInput) > 0.01f);
        animator.SetBool("grounded", isGrounded);
    }

    private void Jump()
    {
        body.linearVelocityY = jumpForce;
        animator.SetTrigger("jump");
    }

    private bool IsGrounded(out Rigidbody2D groundRigidbody)
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(
            boxCollider.bounds.center,
            boxCollider.bounds.size,
            0,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        groundRigidbody = null;

        if (raycastHit.collider == null)
        {
            return false;
        }

        groundRigidbody = raycastHit.rigidbody;
        return true;
    }
}