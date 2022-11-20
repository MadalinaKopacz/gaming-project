using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpSize;
    [SerializeField] private float scale;
    [SerializeField] private Transform firePoint;
    private PlayerScript playerScript;

    private Rigidbody2D body;
    private Animator animator;
    public bool IsGrounded { get; set; }
    public bool IsShooting { get; set; }
    public bool IsMoving {get; set; }

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        IsShooting = false;
        IsMoving = false;
        playerScript = GetComponent<PlayerScript>();
    }

    private void Update()
    {
        bool inverted = playerScript.Inverted;
        float direction = Input.GetAxis("Horizontal");

        if (!inverted)
        {
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

            if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) ) && IsGrounded)
            {
                Jump();
            } 
        }
        else
        {
            body.velocity = new Vector2(-direction * speed, body.velocity.y);

            // flip the player when moving left-right
            if (direction >= 0)
            {
                transform.localScale = scale * new Vector3(-1, 1, 1);
                firePoint.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = scale * Vector3.one;
                firePoint.localScale = Vector3.one;
            }

            if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && IsGrounded)
            {
                Jump();
            }
        }
           
 
        

        
        if (Input.GetButtonDown("Fire1") && IsGrounded)
        {
            IsShooting = true;
            Shoot();
            IsShooting = false;
        }

        if (direction != 0)
        {
            IsMoving = true;
        }
        else 
        {
            IsMoving = false;
        }

        // set the running animation
        animator.SetBool("isRunning", direction != 0);
        animator.SetBool("isGrounded", IsGrounded);
        
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpSize);
        animator.SetTrigger("jump");
        IsGrounded = false;
    }

    private void Shoot()
    {
        animator.SetTrigger("shoot");
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).tagHash);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
            IsGrounded = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (collision.gameObject.CompareTag("obj"))
            IsGrounded = true;
        transform.localRotation = Quaternion.Euler(0, 0, 0);

        if (collision.gameObject.CompareTag("Rat"))
            IsGrounded = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);

    }

    private IEnumerator DelayAction(float delay = 0)
    {
        yield return new WaitForSeconds(delay);
    }
}
