using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnGameStateChangeDropAll;
    }



    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChangeDropAll;
    }

    private void OnGameStateChangeDropAll(DefineGameState gameState)
    {
        if (gameState == DefineGameState.BLOCK_DROPPING)
        {
            if (this.transform.parent.gameObject.GetComponent<LevelManager>().gameMode == DefineGameMode.NORMAL)
            {
                Transform[] childs = new Transform[this.transform.childCount];
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    childs[i] = transform.GetChild(i);

                }
                bool checkExist = false;
                for (int i = 0; i < this.transform.childCount; i++)
                {
                    if (childs[i].gameObject.GetComponent<BlockStatus>().CheckBlockTypeCanHit())
                    {
                        checkExist = true;
                        break;
                    }
                }
                if (!checkExist)
                {
                    GameManager.gameManager.WinGame();
                    return;
                }

                for (int i = 0; i < childs.Length; i++)
                {
                    if (childs[i].GetComponent<BlockStatus>().CheckCanDrop())
                    {
                        childs[i].position += Vector3.down * LevelManager.levelManager.GetDistanceEaBlockScaled();
                    }
                }
                for (int i = 0; i < childs.Length; i++)
                {
                    if (childs[i].position.y <= LevelManager.levelManager.GetLosePosY()+0.01f)
                    {
                        GameManager.gameManager.LoseGame();
                        break;
                    }
                }
                GameManager.gameManager.UpdateGameState(DefineGameState.AIMING);
            }
        }
    }


    public void BombToBlocks()
    {
        Transform[] childs = new Transform[this.transform.childCount];
        for (int i = 0; i < this.transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);

        }
        for (int i = 0; i < childs.Length; i++)
        {
            if (childs[i].gameObject.activeInHierarchy == true)
            {
                childs[i].gameObject.GetComponent<BlockStatus>().Boom();
            }
        }
        StartCoroutine("ItemBombCheckWinGame");
    }
    internal void ChainSawLastRow()
    {
        Transform[] childs = new Transform[this.transform.childCount];
        float minPos = new float();
        for (int i = 0; i < this.transform.childCount; i++)
        {
            childs[i] = transform.GetChild(i);
            if (i == 0)
            {
                minPos = childs[i].transform.position.y;
            }
            else
            {
                if (childs[i].transform.position.y < minPos)
                {
                    minPos = childs[i].transform.position.y;
                }
            }
        }
        for (int i = 0; i < childs.Length; i++)
        {
            if(childs[i].transform.position.y == minPos)
            {
                Destroy(childs[i].gameObject);
            }
        }
        StartCoroutine("ItemBombCheckWinGame");
    }
    IEnumerator ItemBombCheckWinGame()
    {
        if (this.transform.parent.gameObject.GetComponent<LevelManager>().gameMode == DefineGameMode.NORMAL)
        {
            yield return new WaitForSeconds(0.1f); 
            Transform[] childs = new Transform[this.transform.childCount];
            for (int i = 0; i < this.transform.childCount; i++)
            {
                childs[i] = transform.GetChild(i);

            }
            bool checkExist = false;
            for (int i = 0; i < this.transform.childCount; i++)
            {
                if (childs[i].gameObject.GetComponent<BlockStatus>().CheckBlockTypeCanHit())
                {
                    checkExist = true;
                    break;
                }
            }
            if (!checkExist)
            {
                GameManager.gameManager.WinGame();
                yield return null;
            }
        }
    }
}
