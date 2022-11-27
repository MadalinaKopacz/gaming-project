using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Powerup : MonoBehaviour
{
    [SerializeField] private int hpAddition;
    [SerializeField] private int damageAddition;
    [SerializeField] private float speedAddition;
    [SerializeField] private float jumpAddition;
    [SerializeField] private float powerupDuration;
    [SerializeField] private int price;
    

    public int getHpAddition()
    {
        return hpAddition;
    }

    public int getDamageAddition()
    {
        return damageAddition;
    }

    public float getSpeedAddition()
    {
        return speedAddition;
    }

    public float getJumpAddition()
    {
        return jumpAddition;
    }

    public float getPowerupDuration()
    {
        return powerupDuration;
    }

    public int getPowerupPrice()
    {
        return price;
    }
}
