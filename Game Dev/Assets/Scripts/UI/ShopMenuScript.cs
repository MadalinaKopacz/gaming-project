using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ShopMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject shopMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private List<GameObject> items;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private GameObject warningText;
    
    private int currentIdx = 0;
    private GameObject currentItem;
    private Powerup itemStats;

    private void Start()
    {
        currentItem = items[0];
        itemStats = currentItem.GetComponent<Powerup>();
        priceText.SetText(itemStats.getPowerupPrice().ToString());
    }

    public void BuyItem()
    {
        if (playerScript.GetGold() >= itemStats.getPowerupPrice())
        {
            warningText.SetActive(false);
            playerScript.SetGold(playerScript.GetGold() - itemStats.getPowerupPrice());
            playerScript.UsePowerup(currentItem);
            Destroy(currentItem);
            items.RemoveAt(currentIdx);
            NextItem();
        } else
        {
            warningText.SetActive(true);
        }
    }

    public void NextItem()
    {
        currentIdx = (currentIdx + 1) % items.Count;
        currentItem = items[currentIdx];
        displayCurrentItem();
    }

    public void previousItem()
    {
        currentIdx = (currentIdx - 1) % items.Count;
        currentItem = items[currentIdx];
        displayCurrentItem();
    }

    public void returnToPauseMenu()
    {
        warningText.SetActive(false);
        shopMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }

    private void displayCurrentItem()
    {
        warningText.SetActive(false);
        itemStats = currentItem.GetComponent<Powerup>();
        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetActive(false);
        }
        priceText.SetText(itemStats.getPowerupPrice().ToString());
        currentItem.SetActive(true);
    }
}
