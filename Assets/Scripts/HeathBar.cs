using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    public Image heathBar;
    public TextMeshProUGUI heathText;

    public void UpdateHeath(int currentHeath, int maxHeath)
    {
        heathBar.fillAmount = (float)currentHeath / (float)maxHeath;
        heathText.text = $"{currentHeath}/{maxHeath}";
    }
}
