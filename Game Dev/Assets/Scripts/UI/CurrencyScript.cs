using UnityEngine;
using TMPro;

public class CurrencyScript : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI currencyText;
    
    public void setCurrency(int gold)
    {
        currencyText.SetText(gold.ToString());
    }

}
