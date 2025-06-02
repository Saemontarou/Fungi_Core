using UnityEngine;
using UnityEngine.SceneManagement;

public class EscMenu : MonoBehaviour
{
    [SerializeField] private GameObject escMenu;
    [SerializeField] private GameObject aimImage;
    [SerializeField] private GameObject crystals;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (escMenu.activeSelf)
            {
                escMenu.SetActive(false);
                aimImage.SetActive(true);
                crystals.SetActive(true);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            
            else
            {
                escMenu.SetActive(true);
                aimImage.SetActive(false);
                crystals.SetActive(false);
                Time.timeScale = 0f; 
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}