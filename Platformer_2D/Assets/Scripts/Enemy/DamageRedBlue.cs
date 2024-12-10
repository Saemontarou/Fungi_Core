using UnityEngine;

public class DamageRedBlue : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthSystem.health -= 2;
        }
    }
}