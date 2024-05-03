using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    public int numberOfLevel;
    public GameObject levelButtonPrefab;
    public RectTransform parentPanel;
    public int levelReached;

    private void Awake()
    {
        //PlayerPrefs.SetInt("LevelReached",1);
        InitLevelButton();
    }

    private void InitLevelButton()
    {
        if (PlayerPrefs.HasKey("LevelReached"))
        {
            levelReached = PlayerPrefs.GetInt("LevelReached");
        }
        else
        {
            PlayerPrefs.SetInt("LevelReached", 1);
            levelReached = PlayerPrefs.GetInt("LevelReached");
        }

        for(int i = 0; i < numberOfLevel; i++)
        {
            int temp = new int();
            temp = i + 1;
            GameObject levelButton = Instantiate(levelButtonPrefab);
            levelButton.transform.SetParent(parentPanel, false);
            TextMeshProUGUI text = levelButton.GetComponentInChildren<TextMeshProUGUI>();
            levelButton.GetComponent<Button>().onClick.AddListener(delegate
            {
                SelectLevel(temp);
            });
            text.text = (i + 1).ToString();
            if ((i + 1) > levelReached)
            {
                levelButton.GetComponent<Button>().interactable = false;
            }
        }
    }

    void SelectLevel(int index)
    {
        PlayerPrefs.SetInt("LevelSelected", index);
        Debug.Log(index);
        LoadGameplay();
    }
    void LoadGameplay()
    {
        SceneManager.LoadScene("GamePlay");
    }
}
