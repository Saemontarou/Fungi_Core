using UnityEngine;

public class DoorEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            
            Debug.Log("*** THE END ***");
        }
    }
}