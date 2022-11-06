using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate;

    private bool canFire = true;
    private float timer;
    private Camera mainCam;
    private Vector3 mousePos;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

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
            bool playerOnGround = player.GetComponent<PlayerMovement>().GetIsGrounded();

            if (playerOnGround)
            {
                canFire = false;
                Invoke("Shoot", 0.4f); 
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, transform.position, Quaternion.identity);
    }
}
