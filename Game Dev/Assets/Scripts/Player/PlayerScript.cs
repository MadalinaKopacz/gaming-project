using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int hp = 100;

    private bool isHit;
    private float timeSinceLastHit;

    private void Start()
    {
        isHit = false;
        timeSinceLastHit = Time.time;
    }

    void Update()
    {
        if (Time.time >= timeSinceLastHit + 0.8f)
        {
            isHit = false;
            timeSinceLastHit = Time.time;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int damage = 40; // to be changed dynamically when more enemies are implemented
        if (collision.gameObject.CompareTag("Rat"))
        {
            if (!isHit)
            {
                hp -= damage;
                isHit = true;
            }
            if (hp <= 0)
            {
                Debug.Log("Game Over!");
            }
            else
            {
                //nothing for now
            }
        }
    }
}
