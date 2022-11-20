using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    private HealthBarScript healthScript;

    [SerializeField] private GameObject currency;
    private CurrencyScript currencyScript;

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
        currencyScript = currency.GetComponent<CurrencyScript>();
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
            } 
            else
            {
                gameOver.SetActive(false);
            }
        }
       
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            gold = gold + 1;
            currencyScript.setCurrency(gold);

        }
    }

    public int getHp()
    {
        return hp;
    }

    public void setHp(int newHp)
    {
        hp = newHp;
    }

    public int getGold()
    {
        return gold;
    }

    public void setGold(int newGold)
    {
        gold = newGold;
    }

}
