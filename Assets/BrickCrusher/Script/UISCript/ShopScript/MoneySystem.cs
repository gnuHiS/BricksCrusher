using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    #region Singleton
    public static MoneySystem moneySystem;
    private void Awake()
    {
        if (moneySystem == null)
        {
            moneySystem = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        // PlayerPrefs.SetInt("Gems", 100000);
    }
    #endregion
    private void Update()
    {
        //PlayerPrefs.GetInt("Gems");
    }
    public bool UseGems(int price)
    {
        if (PlayerPrefs.GetInt("Gems") >= price)
        {
            PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") - price);
            return true;
        }
        return false;
    }
    
    public void AddGems(int value)
    {
        PlayerPrefs.SetInt("Gems", PlayerPrefs.GetInt("Gems") + value);
    }

    public int GetGems()
    {
        return PlayerPrefs.GetInt("Gems");
    }
}
