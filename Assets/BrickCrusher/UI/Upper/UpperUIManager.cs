using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpperUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI gemsText;

    private void Update()
    {
        gemsText.SetText(MoneySystem.moneySystem.GetGems().ToString());
    }
}
