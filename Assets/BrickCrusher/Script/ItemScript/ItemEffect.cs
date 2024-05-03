using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffect : MonoBehaviour
{
    public static ItemEffect itemEffect;
    public bool isDouble=false;
    [SerializeField] GameObject bounceGround;

    public static event System.Action OnItemUsed;

    private void Awake()
    {
        if (itemEffect == null)
        {
            itemEffect = this;
        }
    }
    public void UseBomb()
    {
        if (PlayerPrefs.GetInt("ItemQuantityBomb") > 0)
        {
            GameManager.gameManager.gameState = DefineGameState.WAITING_4_ITEM;
            LevelManager.levelManager.gameObject.transform.GetComponentInChildren<BlockManager>().BombToBlocks();
            GameManager.gameManager.gameState = DefineGameState.AIMING;
            OnItemUsed?.Invoke();

            PlayerPrefs.SetInt("ItemQuantityBomb", PlayerPrefs.GetInt("ItemQuantityBomb") - 1);
        }
    }


    public void UseDouble()
    {
        if(PlayerPrefs.GetInt("ItemQuantityDouble") > 0)
        {
            isDouble = true;
            LineScript.lineScript.EnableDouble();

            PlayerPrefs.SetInt("ItemQuantityDouble", PlayerPrefs.GetInt("ItemQuantityDouble") - 1);
        }
    }
    public void UseBounce_Ground()
    {
        if (PlayerPrefs.GetInt("ItemQuantityBounce") > 0)
        {
            bounceGround.GetComponent<BounceGround>().InitializeBounceGround();

            PlayerPrefs.SetInt("ItemQuantityBounce", PlayerPrefs.GetInt("ItemQuantityBounce") - 1);
        }
        
    }
    internal void UseChainSaw()
    {
        if (PlayerPrefs.GetInt("ItemQuantityChainSaw") > 0)
        {
            LevelManager.levelManager.gameObject.transform.GetComponentInChildren<BlockManager>().ChainSawLastRow();

            PlayerPrefs.SetInt("ItemQuantityChainSaw", PlayerPrefs.GetInt("ItemQuantityChainSaw") - 1);
        }
    }
    internal void UseBallSpeedUp()
    {
        LevelManager.levelManager.gameObject.transform.GetComponentInChildren<BallController>().BallSpeedUp();
    }
    internal void UseBallComeBack()
    {
        LevelManager.levelManager.gameObject.transform.GetComponentInChildren<BallController>().BallComeBack();
    }
}


