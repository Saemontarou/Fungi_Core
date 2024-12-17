using UnityEngine;

public class DamageFly : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthSystem.health -= 1;
        }
    }
}