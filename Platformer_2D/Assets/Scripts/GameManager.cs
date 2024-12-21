using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

  public GameObject deathScreen;
  public GameObject doorOpenMessage;
  public static GameManager Instance;


  private void Start()
  {
    Instance = this;
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
  }

  public void DoorMessage()
  {
    doorOpenMessage.SetActive(true);
  }
}