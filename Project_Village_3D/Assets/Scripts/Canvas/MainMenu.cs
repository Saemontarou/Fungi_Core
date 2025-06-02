using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayAnimation()
    {
        Invoke("PlayGame", 2f);
    }
    
    public void ExitAnimation()
    {
        Invoke("ExitGame", 2f);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("EXIT GAME");
    }
}