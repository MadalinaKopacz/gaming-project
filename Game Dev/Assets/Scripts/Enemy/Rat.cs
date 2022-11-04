using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    [SerializeField] private int hp = 10;

    //to be implemented in the next sprint
    //[SerializeField] private int damage = 20;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Bullet"))
        {
            int damage = 10;
            hp -= damage;

            if (hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
