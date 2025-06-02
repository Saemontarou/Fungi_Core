using UnityEngine;

public class MapMenu : MonoBehaviour
{
    [SerializeField] private GameObject miniMap;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (miniMap.activeSelf)
            {
                miniMap.SetActive(false);
                Time.timeScale = 1f;
                Cursor.lockState = CursorLockMode.Locked;
            }
            
            else
            {
                miniMap.SetActive(true);
                Time.timeScale = 0f; 
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}