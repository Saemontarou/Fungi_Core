using UnityEngine;

public class DamageHazard : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            HealthSystem.health -= 4;
        }
    }
}