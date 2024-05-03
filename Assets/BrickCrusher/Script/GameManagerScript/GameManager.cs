using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameManager;
    [SerializeField] GameObject canvasWin,canvasLose,topBar;
    [SerializeField] BallController ballController;
    [SerializeField] TopButton topButton;
    [SerializeField] private string[] levelList;

    LevelManager currentLevelManager;
    public DefineGameState gameState;

    public static event System.Action<DefineGameState> OnGameStateChange;

    private void Awake()
    {
        //PlayerPrefs.SetInt("LevelSelected", 4);
        PlayerPrefs.SetInt("ItemQuantityBomb",10);
        PlayerPrefs.SetInt("ItemQuantityDouble",10);
        PlayerPrefs.SetInt("ItemQuantityBounce",10);
        PlayerPrefs.SetInt("ItemQuantityChainSaw",10);
        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        currentLevelManager = Instantiate(Resources.Load("Level/Level " + PlayerPrefs.GetInt("LevelSelected").ToString(), typeof(GameObject)) as GameObject, Vector3.zero, Quaternion.identity).GetComponent<LevelManager>();
        //GameplayCanvas.gameplayCanvas.
    }

    private void Start()
    {
        
        topButton.ResumeGame();
        gameState = DefineGameState.AIMING;
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (true)
        {
            //GameObject levelObject = Instantiate(Resources.Load("Level/Level 01",typeof(GameObject))as GameObject);
        }
    }

    public void UpdateGameState(DefineGameState newState)
    {
        gameState = newState;

        switch (newState)
        {
            case DefineGameState.AIMING:
                
                break;
            case DefineGameState.FIRING:
                
                break;
            case DefineGameState.BLOCK_DROPPING:
                break;
            case DefineGameState.VICTORY:
                break;
            case DefineGameState.LOSE:
                break;
            default:
                break;
        }

        OnGameStateChange?.Invoke(newState);
    }


    public void LoseGame()
    {
        canvasLose.SetActive(true);
        Debug.Log("End Game");
        topButton.PauseGame();
        topBar.SetActive(true);
    }
    public void WinGame()
    {
        canvasWin.SetActive(true);
        Debug.Log("End Game");
        topButton.PauseGame();
        topBar.SetActive(true);

        // Add reward 
        int reward = currentLevelManager.GetReward();
        MoneySystem.moneySystem.AddGems(reward);

        if(PlayerPrefs.GetInt("LevelSelected") == PlayerPrefs.GetInt("LevelReached"))
        {
            PlayerPrefs.SetInt("LevelReached", PlayerPrefs.GetInt("LevelSelected") + 1);
        }
        
    }
}

public enum DefineGameState{
    AIMING,
    FIRING,
    BLOCK_DROPPING,
    WAITING_4_ITEM,
    VICTORY,
    LOSE
}
