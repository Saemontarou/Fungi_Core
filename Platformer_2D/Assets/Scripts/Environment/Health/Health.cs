using System;
using UnityEngine;

public class Health : MonoBehaviour

{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            HealthSystem.health += 2;
            
            
            Debug.Log("YOU PICKED UP THE HEALTH.");
            Destroy(gameObject);
        }
    }
}