using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private GameObject escMenu;
    
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu.activeSelf)
            {
                escMenu.SetActive(false);
            }
            else
            {
                escMenu.SetActive(true);
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // public GameObject pauseMenuUI; 
    //
    // private bool isPaused = false; 
    //
    // void Update() 
    // { 
    //     if (Input.GetKeyDown(KeyCode.Escape)) 
    //     { 
    //         if (isPaused) 
    //         { 
    //             Resume(); 
    //         } 
    //         else 
    //         { 
    //             Pause(); 
    //         } 
    //     } 
    // } 
    //
    // public void Resume() 
    // { 
    //     pauseMenuUI.SetActive(false); 
    //     Time.timeScale = 1f; 
    //     isPaused = false; 
    // } 
    //
    // void Pause() 
    // { 
    //     pauseMenuUI.SetActive(true); 
    //     Time.timeScale = 0f; 
    //     isPaused = true; 
    // } 
    //
    // public void QuitGame() 
    // { 
    //     Application.Quit(); 
    // } 
}