using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    private HealthBarScript healthScript;

    [SerializeField] private GameObject gameOver;

    [SerializeField] private int hp = 100;
    private int gold = 0;

    private bool isHit;
    private float timeSinceLastHit;
    
    
    private void Start()
    {
        healthScript = healthBar.GetComponent<HealthBarScript>();
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
                healthScript.setHealth();
            }
            if (hp <= 0)
            {
                gameOver.SetActive(true);
                Button menuButton = gameOver.transform.Find("MenuButton").gameObject.GetComponent<Button>();
                menuButton.onClick.AddListener(() => {
                    resetStats();
                    SceneManager.LoadScene(0);
                });
            } 
            else
            {
                gameOver.SetActive(false);
            }
        }
    }

    public int getHp()
    {
        return hp;
    }

    public int getGold()
    {
        return gold;
    }

    public void setGold(int newGold)
    {
        gold = newGold;
    }

    private void resetStats()
    {
        hp = 100;
    }
}
