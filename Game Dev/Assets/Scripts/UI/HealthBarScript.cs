
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    [SerializeField] private Slider slider;
    
    [SerializeField] private GameObject healthTextObject;
    private Text healthText;

    [SerializeField] private GameObject player;
    private PlayerScript playerStats;


    private void Start()
    {
        healthText = healthTextObject.GetComponent<Text>();
        playerStats = player.GetComponent<PlayerScript>();
        setMaxHealth();
    }

    public void setMaxHealth()
    {
        slider.maxValue = playerStats.getHp();
        slider.value = playerStats.getHp();
    }

    public void setHealth()
    {
        healthText.text = playerStats.getHp() + "/100"; 
        slider.value = playerStats.getHp();
    }
}
