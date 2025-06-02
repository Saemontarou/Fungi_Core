using UnityEngine;

public class PauseHandler : MonoBehaviour
{
   [SerializeField] private GameObject escMenu;
  
   private void OnEnable()
   {
      GameManager.OnPause += HandlePause;
      GameManager.OnResume += HandleResume;
   }

   private void HandlePause()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (escMenu.activeSelf)
         {
            escMenu.SetActive(false);
            Time.timeScale = 1;
         }
      }
   }

   private void HandleResume()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         if (escMenu.activeSelf)
         {
            escMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
         }
      }
   }

   private void OnDisable()
   {
      GameManager.OnPause -= HandlePause;
      GameManager.OnResume -= HandleResume;
   }
}