using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBird : MonoBehaviour
{



    
    private void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("obj");
        Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void  OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        
    }


}
