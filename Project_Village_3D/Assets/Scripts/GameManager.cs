using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject aimImage;
    public GameObject crystals;

    // [Header("Башни с кристаллами")] 
    // public GameObject towerRedHead;
    // public GameObject towerGreenHead;
    // public GameObject towerBlueHead;
    // public GameObject towerOrangeHead;
    // public GameObject towerPurpleHead;

    public GameObject enemyBossOne;
    //public GameObject enemyBossTwo;

    public GameObject deathZone;
    
    public static GameManager Instance;

    public static event Action OnPause;
    public static event Action OnResume;

    private void Start()
    {
        Instance = this;
    }

    public void TogglePause(bool bIsPause)
    {
        if (bIsPause)
        {
            OnPause?.Invoke();
        }
        else
        {
            OnResume?.Invoke();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Debug.Log("EXIT GAME");
        Application.Quit();
    }

    public void Lose()
    {
        deathScreen.SetActive(true);
        aimImage.SetActive(false);
        crystals.SetActive(false);
        
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // public void TowerHeadVisible()
    // {
    //     towerRedHead.SetActive(true);
    //     towerGreenHead.SetActive(true);
    //     towerBlueHead.SetActive(true);
    //     towerOrangeHead.SetActive(true);
    //     towerPurpleHead.SetActive(true);
    // }

    public void BossesVisible()
    {
        enemyBossOne.SetActive(true);
        deathZone.SetActive(true);
        
        //enemyBossTwo.SetActive(true);
    }
    
    
}
