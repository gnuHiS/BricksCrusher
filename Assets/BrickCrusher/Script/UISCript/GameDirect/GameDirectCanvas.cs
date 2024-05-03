using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameDirectCanvas : MonoBehaviour
{
    [SerializeField] GameObject loseCanvas;
    public void HomeButton()
    {
        SceneManager.LoadScene("Home");
    }
    public void ClaimButton()
    {
        SceneManager.LoadScene("Home");
    }
    public void RemoveThreeLinesButton()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        TopButton.gameIsPaused = false;
        loseCanvas.SetActive(false);
        StartCoroutine("Remove3Lines");
    }
    IEnumerator Remove3Lines()
    {
        for(int i = 0; i < 3; i++)
        {
            ItemEffect.itemEffect.UseChainSaw();
            yield return new WaitForSeconds(0.05f);
        }
    }
}
