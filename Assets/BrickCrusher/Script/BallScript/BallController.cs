using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    // var- Const
    const float minAngle = -85f, maxAngle = 85f;
    Vector2 minAngleVector = new Vector2(Mathf.Tan(Mathf.Deg2Rad*85), 1), maxAngleVector = new Vector2(-Mathf.Tan(Mathf.Deg2Rad * 85), 1);

    // var- Input Saver
    private Vector2 startPos,mouseEndPos,finalEndPos;
    private bool canDrag;

    // var- Calculated Direction
    private Vector2 finalDirection;

    // var- Delta Y,X -- Angle B
    private float diffY, diffX, angleB;

    // var- Shot
    GameObject[] ballGroup;
    private int totalBall;
    private int currentBallTaken = 0;
    private const float timeBetweenFire = 0.1f;
    

    private bool isFastForward;

 
    private void OnEnable()
    {
        GameManager.OnGameStateChange += OnStateChangeSetAiming;
        
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChange -= OnStateChangeSetAiming;
    }

    // Start is called before the first frame update
    void Start()
    {

        
        startPos = new Vector3(transform.position.x, LevelManager.levelManager.GetBallPosYScaled(), transform.position.z);

        
        LineScript.lineScript.GetReferenceVar(startPos, LevelManager.levelManager.GetBallSpeed(), minAngle, maxAngle);

        canDrag = false;
        

        totalBall = LevelManager.levelManager.GetNumberInitialBall();

        ballGroup = new GameObject[totalBall*2];
        for (int i = 0; i < totalBall*2; i++)
        {
            ballGroup[i] = (GameObject)Instantiate(Resources.Load("Ball/Ball"), startPos, Quaternion.identity);
            ballGroup[i].GetComponent<BallMovement>().SetRefToController(this);
            ballGroup[i].transform.localScale = new Vector3(ballGroup[i].transform.localScale.x,
                                                            ballGroup[i].transform.localScale.y,
                                                            ballGroup[i].transform.localScale.z) * LevelManager.levelManager.GetLevelScale();
        }
        GroupChild();
        DisableDouble();
        isFastForward = false;
    }

    internal void BallComeBack()
    {
        StopCoroutine("Fire");
        if (!ItemEffect.itemEffect.isDouble)
        {
            for(int i = 0; i < totalBall; i++)
            {
                ballGroup[i].GetComponent<BallMovement>().StopBall();
                if (i != totalBall-1)
                {
                    ballGroup[i].GetComponent<BallMovement>().ControlledOnReNew(false, startPos);
                }
                else if (i == totalBall - 1)
                {
                    ballGroup[i].GetComponent<BallMovement>().ControlledOnReNew(true, startPos);
                }
            }
        }
        else if (ItemEffect.itemEffect.isDouble)
        {
            for (int i = 0; i < totalBall*2; i++)
            {
                ballGroup[i].GetComponent<BallMovement>().StopBall();
                if (i != totalBall*2 - 1)
                {
                    ballGroup[i].GetComponent<BallMovement>().ControlledOnReNew(false, startPos);
                }
                else if (i == totalBall*2 - 1)
                {
                    ballGroup[i].GetComponent<BallMovement>().ControlledOnReNew(true, startPos);
                }
            }
        }
    }

    internal void BallSpeedUp()
    {
        if (!isFastForward)
        {
            isFastForward = true;
            for(int i = 0; i < totalBall; i++)
            {
                ballGroup[i].GetComponent<BallMovement>().FastForward();
                if (ItemEffect.itemEffect.isDouble)
                {
                    ballGroup[i+totalBall].GetComponent<BallMovement>().FastForward();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TopButton.gameIsPaused)
        {
            return;
        }
        switch (GameManager.gameManager.gameState)
        {
            case DefineGameState.AIMING:
                // On Tapped
                if (Input.GetMouseButtonDown(0))
                {
                    OnTapped();
                }
                // Dragging
                if (Input.GetMouseButton(0) && canDrag)
                {
                    Dragging();
                }
                // On Released
                if (Input.GetMouseButtonUp(0) && canDrag)
                {
                    OnReleased();
                }
                break;

            case DefineGameState.FIRING:
                break;

            default:
                break;
        }
        
    }
    
    #region InnerUpdate
    // func- On Tapped
    private void OnTapped()
    {
        if (Camera.main.ScreenToWorldPoint(Input.mousePosition).y > LevelManager.levelManager.GetBallPosYScaled()-LevelManager.levelManager.GetBallRadiusScaled() &&
            Camera.main.ScreenToWorldPoint(Input.mousePosition).y < 7.27f)
        {
            // Show the Preview Line
            LineScript.lineScript.ActivateDots();

            // Switch accept mouse input
            canDrag = true;
            if (ItemEffect.itemEffect.isDouble)
            {
                EnableDouble();
            }
        }
    }

    // func- Dragging
    private void Dragging()
    {
        GetMouseInput();
        
        CalculateFinalDirection();

        LineScript.lineScript.CircleCasting(finalDirection, angleB);

        LineScript.lineScript.RotateLine();
    }

    // func- On Released
    private void OnReleased()
    {
        LineScript.lineScript.DeActivateDots();

        GameManager.gameManager.UpdateGameState(DefineGameState.FIRING);

        StartCoroutine("Fire");

        canDrag = false;
        LineScript.lineScript.DisableDouble();
    }
    #endregion

    private void GroupChild()
    {
        for (int i = 0; i < totalBall*2; i++)
        {
            ballGroup[i].transform.SetParent(this.transform);
        }
    }

    // func- Fire
    private IEnumerator Fire()
    {
        int i = 0;
        while (i < totalBall)
        {
            ballGroup[i].GetComponent<BallMovement>().ControlledFire(LevelManager.levelManager.GetBallSpeed(), finalDirection);
            if (ItemEffect.itemEffect.isDouble)
            {
                ballGroup[i + totalBall].GetComponent<BallMovement>().ControlledFire(LevelManager.levelManager.GetBallSpeed(), new Vector2(-finalDirection.x, finalDirection.y));
            }
            yield return new WaitForSeconds(timeBetweenFire);
            i++;
        }
        yield return new WaitForSeconds(1f);
        GameplayCanvas.gameplayCanvas.TurnOnButtonSpeedUp();
    }


    // func- Get Mouse Input
    private void GetMouseInput()
    {
        mouseEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        diffX = mouseEndPos.x - startPos.x;
        diffY = mouseEndPos.y - startPos.y; if (diffY == 0) diffY = 0.0001f;
        angleB = -Mathf.Atan2(diffX, diffY) * Mathf.Rad2Deg;
    }
    

    // func- Calc Direction --- Calc Final Direction
    private void CalculateFinalDirection()
    {
        if (angleB > maxAngle)
        {
            finalDirection = maxAngleVector.normalized;
        }
        else if (angleB < minAngle)
        {

            finalDirection = minAngleVector.normalized;
        }
        else
        {
            finalEndPos = mouseEndPos;
            finalDirection = new Vector2(finalEndPos.x - startPos.x, finalEndPos.y - startPos.y).normalized;
        }
    }


    // public func- ReNew each Turn
    public void ReNew(Vector3 ballPos, BallMovement ballMovement)
    {
        if (!ItemEffect.itemEffect.isDouble)
        {
            currentBallTaken += 1;

            if (currentBallTaken == 1)
            {
                startPos = ballPos;
            }

            if (currentBallTaken == totalBall)
            {
                ballMovement.ControlledOnReNew(true, startPos);
                for(int i = 0; i < totalBall; i++)
                {
                    ballGroup[i + totalBall].transform.position = startPos;
                }
            }
            else if (currentBallTaken != 1)
            {
                ballMovement.ControlledOnReNew(false, startPos);
            }
        }
        else if (ItemEffect.itemEffect.isDouble)
        {
            currentBallTaken += 1;

            if (currentBallTaken == 1)
            {
                startPos = ballPos;
            }

            if (currentBallTaken == totalBall*2)
            {
                ballMovement.ControlledOnReNew(true, startPos);
                
            }
            else if (currentBallTaken != 1)
            {
                ballMovement.ControlledOnReNew(false, startPos);
            }
        }
    }
    public void ResetTurn()
    {
        currentBallTaken = 0;
        isFastForward = false;
        ItemEffect.itemEffect.isDouble = false;
    }

    private void OnStateChangeSetAiming(DefineGameState gameState)
    {
        if(gameState == DefineGameState.AIMING)
        {
            // ReSetPos
            LineScript.lineScript.SetDotsPosDefault(startPos);
            // Show the Preview Line
            LineScript.lineScript.ActivateDots();
            for (int i = 0; i < totalBall * 2; i++)
            {
                ballGroup[i].GetComponent<BallMovement>().canHit = true;
            }
        }
    }
    private void EnableDouble()
    {
        for(int i = totalBall; i < totalBall * 2; i++)
        {
            ballGroup[i].SetActive(true);
        }
    }
    internal void DisableDouble()
    {
        for (int i = totalBall; i < totalBall * 2; i++)
        {
            ballGroup[i].SetActive(false);
        }
    }
}

/*
 -------------------------------------------------To Do List-----------------------------------------------
    
*/