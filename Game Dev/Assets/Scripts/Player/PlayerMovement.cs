using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSize;
    [SerializeField] private float scale;
    [SerializeField] private Transform firePoint;

    private Rigidbody2D body;
    private Animator animator;
    private bool isGrounded;
    private bool isShooting = false;

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
    }

    private void Update()
    {
        float direction = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(direction * speed, body.velocity.y);

        // flip the player when moving left-right
        if (direction >= 0)
        {
            transform.localScale = scale * Vector3.one;
            firePoint.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = scale * new Vector3(-1, 1, 1);
            firePoint.localScale = new Vector3(-1, 1, 1);
        }

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) ) && isGrounded)
        {
            Jump();
        }
        
        if (Input.GetButtonDown("Fire1") && isGrounded)
        {
            isShooting = true;
            Shoot();
            isShooting = false;
        }

        // set the running animation
        animator.SetBool("isRunning", direction != 0);
        animator.SetBool("isGrounded", isGrounded);
        
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSize);
        animator.SetTrigger("jump");
        isGrounded = false;
    }

    private void Shoot()
    {
        animator.SetTrigger("shoot");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
            isGrounded = true;

    }
}
