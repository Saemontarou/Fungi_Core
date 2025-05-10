using UnityEngine;

public class DamageByPlayer : MonoBehaviour

{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Destroy(this.gameObject);
        }
        Debug.Log("DESTROY MAIN OBJECT");
    }
}