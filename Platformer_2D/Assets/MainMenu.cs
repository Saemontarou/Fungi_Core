using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
   {
      SceneManager.LoadScene("MainScene");
   }

   public void ExitGame()
   {
      Debug.Log("EXIT GAME");
      Application.Quit();
   }
}