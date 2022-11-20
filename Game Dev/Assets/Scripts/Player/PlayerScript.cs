using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private GameObject healthBar;
    private HealthBarScript healthScript;

    [SerializeField] private GameObject currency;
    private CurrencyScript currencyScript;

    [SerializeField] private GameObject gameOver;

    [SerializeField] private int hp = 100;
    private int gold = 0;
    
    public int damagePerHit { get; set; }

    private bool isHit;
    private float timeSinceLastHit;
    public bool Inverted { get; set; }
    
    private void Start()
    {
        healthScript = healthBar.GetComponent<HealthBarScript>();
        isHit = false;
        timeSinceLastHit = Time.time;
        currencyScript = currency.GetComponent<CurrencyScript>();
        damagePerHit = 10;
        Inverted = false;
    }

    void Update()
    {
        if (Time.time >= timeSinceLastHit + 0.8f)
        {
            isHit = false;
            timeSinceLastHit = Time.time;
        }
    }

    private IEnumerator OnCollisionEnter2D(Collision2D collision)
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

        if (collision.gameObject.CompareTag("Mushroom"))
        {
            Destroy(collision.gameObject);
            Inverted = true;
            yield return new WaitForSeconds(5);
            Inverted = false;

        }

        if (collision.gameObject.CompareTag("Powerup"))
        {
            // Activate powerup and destroy object
            usePowerup(collision.gameObject);
            Destroy(collision.gameObject);
        }
    }

    private void usePowerup(GameObject powerup)
    {
        // Hp is added without being removed later
        powerupHp(powerup);
        int restoreDamagePerHit = powerupDamage(powerup);
        float restoreSpeed = powerupSpeed(powerup);
        float restoreJump = powerupJump(powerup);

        float duration = powerup.GetComponent<Powerup>().getPowerupDuration();

        // When powerup duration expires restore values back
        StartCoroutine(restoreValuesPowerup(duration, restoreDamagePerHit, 
            restoreSpeed, restoreJump));
    }

    IEnumerator restoreValuesPowerup(float delayTime, int oldDamagePerHit, 
        float oldSpeed, float oldJumpSize)
    {
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        
        // Restore values
        damagePerHit = oldDamagePerHit;
        GetComponent<PlayerMovement>().setSpeed(oldSpeed);
        GetComponent<PlayerMovement>().setJumpSize(oldJumpSize);
    }

    private void powerupHp(GameObject powerup)
    {
        // Get hp bonus and add it without overflowing healthbar
        int addHp = powerup.GetComponent<Powerup>().getHpAddition();
        if (addHp > 0)
        {
            if (addHp + hp > 100)
            {
                setHp(100);
            }
            else
            {
                setHp(addHp + hp);
            }
        }
        healthScript.setHealth();
    }

    private int powerupDamage(GameObject powerup)
    {
        // Get damage bonus from powerup and add it
        // return the old value 
        int addDamage = powerup.GetComponent<Powerup>().getDamageAddition();
        if (addDamage > 0)
        {
            int beforePowerup = damagePerHit;
            damagePerHit += addDamage;
            return beforePowerup;
        }
        return damagePerHit;
    }

    private float powerupSpeed(GameObject powerup)
    {
        // Get speed bonus from powerup and add it
        // return the old value 
        float addSpeed = powerup.GetComponent<Powerup>().getSpeedAddition();
        float beforePowerup = GetComponent<PlayerMovement>().getSpeed();
        if (addSpeed > 0)
        {
            GetComponent<PlayerMovement>().setSpeed(beforePowerup + addSpeed);
        }
        return beforePowerup;
    }

    private float powerupJump(GameObject powerup)
    {
        // Get jump size bonus from powerup and add it
        // return the old value 
        float addJumpSize = powerup.GetComponent<Powerup>().getJumpAddition();
        float beforePowerup = GetComponent<PlayerMovement>().getJumpSize();
        if (addJumpSize > 0)
        {
            GetComponent<PlayerMovement>().setJumpSize(beforePowerup + addJumpSize);
        }
        return beforePowerup;
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
