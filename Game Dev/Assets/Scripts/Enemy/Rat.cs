using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : MonoBehaviour
{
    public int hp = 10;
    public int damage = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
