using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBird : MonoBehaviour
{



    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            Destroy(gameObject);
        //if (other.CompareTag("Player"))
            //Destroy(gameObject);
    }*/


    void  OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }


}
