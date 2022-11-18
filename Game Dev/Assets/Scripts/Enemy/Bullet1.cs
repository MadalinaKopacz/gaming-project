using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    GameObject target;

    [SerializeField] private float speed;

    Rigidbody2D bulletRB;


    /*void Start()
    {
        bulletRB = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bulletRB.velocity = new Vector2(moveDir.x, moveDir.y);
        print(target.transform.position);
        //Destroy(gameObject, 2);
    }*/
    


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            Destroy(gameObject);
        if (other.CompareTag("player"))
            Destroy(gameObject);
    }
    
}
