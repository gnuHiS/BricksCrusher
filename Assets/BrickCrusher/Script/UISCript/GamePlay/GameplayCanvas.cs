using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameplayCanvas : MonoBehaviour
{
    public static GameplayCanvas gameplayCanvas;

    [SerializeField] GameObject itemGroupCanvas, ballButtonGroupCanvas;
    [SerializeField] GameObject buttonSpeedUp;
    [SerializeField] GameObject buttonComeback;
    [SerializeField] GameObject[] quantityText;
    public TextMeshProUGUI textLevel;

    

    private void Awake()
    {
        if (gameplayCanvas == null)
        {
            gameplayCanvas = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateTextNumber();
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += ShowHideButtonGroup;
        ItemEffect.OnItemUsed += ItemUsed;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChange -= ShowHideButtonGroup;
        ItemEffect.OnItemUsed -= ItemUsed;
    }
    private void ItemUsed()
    {
        UpdateTextNumber();
    }

    private void ShowHideButtonGroup(DefineGameState state)
    {
        if (state == DefineGameState.AIMING)
        {
            itemGroupCanvas.SetActive(true);
            ballButtonGroupCanvas.SetActive(false);
        }
        else if(state == DefineGameState.FIRING)
        {
            TurnOnButtonComeback();
            itemGroupCanvas.SetActive(false);
            ballButtonGroupCanvas.SetActive(true);
            buttonSpeedUp.GetComponent<Button>().interactable = false;
            buttonSpeedUp.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
        }

    }
    public void TurnOnButtonSpeedUp()
    {
        buttonSpeedUp.GetComponent<Button>().interactable = true;
        buttonSpeedUp.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
    }

    internal void TurnOffButtonComeback()
    {
        buttonComeback.GetComponent<Button>().interactable = false;
        buttonSpeedUp.transform.GetChild(0).GetComponent<Image>().fillAmount = 0;
    }
    internal void TurnOnButtonComeback()
    {
        buttonComeback.GetComponent<Button>().interactable = true;
        buttonSpeedUp.transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
    }
    internal void UpdateTextNumber()
    {
        quantityText[0].GetComponent<TextMeshProUGUI>().SetText("x " + PlayerPrefs.GetInt("ItemQuantityBomb"));
        quantityText[0].GetComponent<TextMeshProUGUI>().SetText("x " + PlayerPrefs.GetInt("ItemQuantityDouble"));
        quantityText[0].GetComponent<TextMeshProUGUI>().SetText("x " + PlayerPrefs.GetInt("ItemQuantityBounce"));
        quantityText[0].GetComponent<TextMeshProUGUI>().SetText("x " + PlayerPrefs.GetInt("ItemQuantityChainSaw"));
    }
}