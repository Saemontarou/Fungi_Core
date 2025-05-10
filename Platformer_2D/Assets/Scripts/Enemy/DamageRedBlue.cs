using System.Security.Cryptography;
using UnityEngine;

public class DamageRedBlue : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthSystem.health -= 1;
        }

        if (collision.gameObject.tag == "Axe")
        {
            Destroy(this.gameObject);
        }
    }
}