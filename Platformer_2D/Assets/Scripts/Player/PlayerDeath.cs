using UnityEngine;

public class PlayerDeath : MonoBehaviour

{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            HealthSystem.health -= 1;
        }

        if (HealthSystem.health <= 0)
        {
            Destroy(this.gameObject);
            GameManager.Instance.Lose();
            Time.timeScale = 0;
        }
    }
}