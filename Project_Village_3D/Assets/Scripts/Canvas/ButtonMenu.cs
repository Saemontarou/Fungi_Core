using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonMenu : MonoBehaviour

{
    public void RestartGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}