using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float speed;
	public float jumpForce;

	public bool isGrounded;
	private Rigidbody2D body;
	private Animator animator;
	private void Awake()
	{
		body = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
        float horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        body.linearVelocityX = horizontalInput * speed;
		if (Input.GetKeyDown(KeyCode.Space) && isGrounded )
			Jump();

        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded);
	}
	private void Jump()
	{
		body.linearVelocityY = jumpForce;
        isGrounded = false;
        animator.SetTrigger("jump");
	}


	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Ground")
			isGrounded = true; 
	}
}