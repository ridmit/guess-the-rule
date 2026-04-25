using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;
	public float jumpForce;

    [SerializeField] private LayerMask groundLayer;
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
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        body.linearVelocityX = horizontalInput * speed;
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
			Jump();

        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());
	}
	private void Jump()
	{
		body.linearVelocityY = jumpForce;
        animator.SetTrigger("jump");
	}

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}