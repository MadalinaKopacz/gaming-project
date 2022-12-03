using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;
    [SerializeField] private float aimingTime;
    [SerializeField] private Transform player;

    private bool canFire = true;
    private SpriteRenderer spriteRenderer;
    private float timer;
    private Camera mainCam;
    private Vector3 mousePos;
    private GameObject playerArm;
    private float aimingTimeStamp;

    private Animator animator;

    public bool isAiming { get; set; }

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        spriteRenderer = player.GetComponent<SpriteRenderer>();
        playerArm = GameObject.Find("/Player/ArmPivot/ArmShoot");
        playerArm.SetActive(false);
        isAiming = false;
        animator = player.GetComponent<Animator>();
        animator.SetBool("isAiming", false);
    }

    private void setAiming(bool value)
    {
        isAiming = value;
        playerArm.SetActive(value);
        animator.SetBool("isAiming", value);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseDelta = mainCam.ScreenToWorldPoint(Input.mousePosition) - mousePos;
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        bool playerMoving = player.GetComponent<PlayerMovement>().IsMoving;
        bool playerShooting = player.GetComponent<PlayerMovement>().IsShooting;

        // make player look where he aims
        // consider aiming when user changed mouse position
        if (playerMoving) 
        {
            if (playerShooting == false)
            {
                setAiming(false);
            }
        }
        else if ((mouseDelta.x == 0 && mouseDelta.y == 0) && isAiming == true)
        {
            // check if player just stopped aiming/moving mouse
            if (Time.time - aimingTimeStamp >= aimingTime) 
            {
                // exit aiming, arm is down
                setAiming(false);
            }
        } else if ((mouseDelta.x != 0 || mouseDelta.y != 0) || isAiming == true)
        {
            // flip player to look in the arm direction
            if(mousePos.x < player.position.x)
            {
                player.eulerAngles = new Vector3(0, 180, 0);
            }
            else
            {
                player.eulerAngles = new Vector3(0, 0, 0);
            }

            // actualize timestamp for the last time player moved mouse
            aimingTimeStamp = (isAiming == false) ? Time.time : aimingTimeStamp;

            // aiming in motion, arm is shown
            setAiming(true);
        } else if (isAiming == false)
        {
            // make sure arm is not shown
            playerArm.SetActive(false);
        }

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > fireRate)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetButtonDown("Fire1") && canFire)
        {
            GameObject player = GameObject.Find("Player");
            bool playerOnGround = player.GetComponent<PlayerMovement>().IsGrounded;

            if (playerOnGround)
            {
                if (playerMoving)
                {
                    setAiming(true);
                    player.GetComponent<PlayerMovement>().IsShooting = true;
                    canFire = false;
                    
                    aimingTimeStamp = Time.time;
                    animator.CrossFade("ShootRunning", 0.4f);
                    Invoke("Shoot", 0.4f); 
                    player.GetComponent<PlayerMovement>().IsShooting = false;
                    setAiming(false);
                }
                else 
                {
                    player.GetComponent<PlayerMovement>().IsShooting = true;
                    animator.SetTrigger("shoot");
                    canFire = false;
                    
                    // player started shooting, so animation is in motion
                    // make sure they stop aiming so arm is not shown
                    aimingTimeStamp = Time.time;
                    Invoke("Shoot", 0.4f); 
                    player.GetComponent<PlayerMovement>().IsShooting = false;
                }
            }
        }
    }

    private void Shoot()
    {       
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
