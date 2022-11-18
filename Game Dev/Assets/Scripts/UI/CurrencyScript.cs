using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class CurrencyScript : MonoBehaviour
{

    public TextMeshProUGUI currencyText;

    public void setCurrency(int gold)
    {
        currencyText.SetText(gold.ToString());
    }

}
