using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collScript : MonoBehaviour
{
   /* void Start()
    {
        GameObject caca = GameObject.FindGameObjectWithTag("caca");
        Physics2D.IgnoreCollision(caca.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    void Update()
    {
        GameObject caca = GameObject.FindGameObjectWithTag("caca");
        Physics2D.IgnoreCollision(caca.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    } */

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject caca = GameObject.FindGameObjectWithTag("caca");
        if (collision.gameObject.CompareTag("caca"))
        {
            Physics2D.IgnoreCollision(caca.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }


    }

}