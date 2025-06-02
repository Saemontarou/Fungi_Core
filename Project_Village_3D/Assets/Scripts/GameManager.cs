using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject aimImage;
    public GameObject crystals;
    
    public static event Action OnPause;
    public static event Action OnResume;
    
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
        SceneManager.LoadScene("MainMenu");
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
}