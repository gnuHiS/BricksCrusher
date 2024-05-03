using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class Shop : MonoBehaviour
{
    private string PurchasedPath;
    [System.Serializable] class ShopItem 
    {
        [SerializeField] BallScriptable ballScriptableObject;
        public bool isPurchased = false;

        public Sprite GetImage()
        {
            return ballScriptableObject.artWork;
        }
        public int GetPrice()
        {
            return ballScriptableObject.price;
        }
        public bool CheckNull()
        {
            if (ballScriptableObject == null)
            {
                return true;
            }
            return false;
        }
    }

    [SerializeField] List<ShopItem> ShopItemsList;
    
    [SerializeField] Animator NoGemAnim;

    [SerializeField] GameObject ItemTemplate;
    GameObject[] itemViews;

    GameObject g;
    [SerializeField] Transform content;
    Button buyBtn;

    private void Awake()
    {
        PurchasedPath = $"{Application.persistentDataPath}/PurchasedMatrix.json";
    }

    private void Start()
    {
        itemViews = new GameObject[ShopItemsList.Count];
        for(int i = 0; i < ShopItemsList.Count; i++)
        {
            if (!ShopItemsList[i].CheckNull())
            {
                itemViews[i] = Instantiate(ItemTemplate, content);
                itemViews[i].transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].GetImage();
                itemViews[i].transform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>().text = ShopItemsList[i].GetPrice().ToString();
                buyBtn = itemViews[i].transform.GetChild(2).GetComponent<Button>();
                buyBtn.interactable = !ShopItemsList[i].isPurchased;
                buyBtn.AddEventListener(i, OnShopItemBtnClick);
            }
        }
    }
    void OnShopItemBtnClick(int itemIndex)
    {
        if (MoneySystem.moneySystem.UseGems(ShopItemsList[itemIndex].GetPrice()))
        {
            ShopItemsList[itemIndex].isPurchased = true;
            buyBtn = content.GetChild(itemIndex).GetChild(2).GetComponent<Button>();
            buyBtn.interactable = false;
            buyBtn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "PURCHASED";
        }
        else
        {
            NoGemAnim.SetTrigger("NoGems");
            Debug.Log("You are so poor !");
        }
    }
}
