using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    public int level;

    [Header("Select Game Mode:")]
    public DefineGameMode gameMode;

    [Header("Level Attribute:")]
    [SerializeField] private float levelScale = 1;

    [Header("Block Manager")]
    [SerializeField] private float defaultDistanceEaBlock;

    [Header("Ball Controller")]
    [SerializeField] private float defaultBallPosY;
    [SerializeField] private float defaultBallRadius;
    [SerializeField] private int numberInitialBall;
    [SerializeField] private float ballSpeed;

    [SerializeField] private int reward;


    private void Awake()
    {
        if (levelManager == null)
        {
            levelManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        GameplayCanvas.gameplayCanvas.textLevel.text = "Level " + level.ToString();
    }

    internal float GetLevelScale()
    {
        return levelScale;
    }

    internal float GetDistanceEaBlockScaled()
    {
        return defaultDistanceEaBlock * levelScale;
    }

    internal float GetLosePosY()
    {
        Debug.Log(GetBallPosYScaled() - GetBallRadiusScaled() + GetDistanceEaBlockScaled() / 2);
        return GetBallPosYScaled() - GetBallRadiusScaled() + GetDistanceEaBlockScaled() / 2;
    }

    internal float GetBallPosYScaled()
    {
        return defaultBallPosY - (defaultBallRadius - GetBallRadiusScaled());
    }

    internal float GetBallRadiusScaled()
    {
        return defaultBallRadius * levelScale;
    }
    internal int GetNumberInitialBall()
    {
        return numberInitialBall;
    }
    internal float GetBallSpeed()
    {
        return ballSpeed;
    }
    internal int GetReward()
    {
        return reward;
    }
}

public enum DefineGameMode
{
    NORMAL,
    ENDLESS
}