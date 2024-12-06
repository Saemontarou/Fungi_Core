using System;
using UnityEngine;

public class OpenKey : MonoBehaviour
{

    public static Action OnDoorOpen;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            OnDoorOpen?.Invoke();
            
            Debug.Log("HURRY UP! HEAVEN'S DOOR IS OPEN!");
            Destroy(gameObject);
        }
    }
}