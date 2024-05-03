using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceGround : MonoBehaviour
{
    private int chanceLeft;
    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnGameStateChangeDropAll;
    }



    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnGameStateChangeDropAll;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball") && collision.gameObject.GetComponent<BallMovement>().GetLastVeloY() < 0)
        {
            chanceLeft -= 1;
            if (chanceLeft == 0)
            {
                this.gameObject.SetActive(false);
            }
        }
    }
    public void InitializeBounceGround()
    {
        this.gameObject.SetActive(true);
        //chanceLeft = LevelManager.levelManager.GetNumberInitialBall();
        chanceLeft = 50;
    }
    private void OnGameStateChangeDropAll(DefineGameState gameState)
    {
        if (gameState == DefineGameState.BLOCK_DROPPING)
        {
            this.gameObject.SetActive(false);
        }

    }
}
