using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ShopMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject shopMenuUI;
    [SerializeField] private GameObject pauseMenuUI;
    private List<GameObject> items;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private PlayerScript playerScript;
    [SerializeField] private GameObject warningText;
    
    private int currentIdx = 0;
    private GameObject currentItem;
    private Powerup itemStats;

    // Problem: when the list "items" should be emptied and new ones added
    // only the objects get destroyed, the reference in the array stays
    // so error MissingReferenceException: The variable items of ShopMenuScript doesn't exist anymore.
    // You probably need to reassign the items variable of the 'ShopMenuScript' script in the inspector.

    public void setItems(List<GameObject> newItems)
    {
        if (items != null && items.Count > 0) {
            foreach(GameObject u in items) {
                Destroy(u);
            }
            items.Clear();
        }

        items = new List<GameObject>();
        foreach(GameObject go in newItems) 
        {
            items.Add(go);
        }

        currentIdx = 0;
        currentItem = items[0];

        itemStats = currentItem.GetComponent<Powerup>();
        priceText.SetText(itemStats.getPowerupPrice().ToString());
        displayCurrentItem();
    }

    private void Start()
    {
        items = new List<GameObject>();
        GameObject wrapper = GameData.FindGameObjectInScene("ItemsList");
        foreach (Transform child in wrapper.transform)
        {
            child.gameObject.SetActive(false);
            items.Add(child.gameObject);
        }
        
        currentIdx = 0;
        currentItem = items[0];
        currentItem.SetActive(true);
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
        currentIdx--;
        if (currentIdx < 0)
        {
            currentIdx = items.Count - 1;
        }
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

    public List<GameObject> getItems()
    {
        return new List<GameObject>(items);
    }
}
