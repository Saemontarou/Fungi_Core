using UnityEngine;

public class HealthRestoration : MonoBehaviour
{
    private int _health = 25;
    [SerializeField] private GameObject playerHealth;
    private PlayerHealth _takeHealth;
    
    private void Start()
    {
        if (playerHealth != null)
        {
            _takeHealth = playerHealth.GetComponent<PlayerHealth>();
        }
    }
    
    public void HealthZone()
    {
        _takeHealth.TakeHealth(_health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating(nameof(HealthZone), 1, 1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke(nameof(HealthZone));
    }
}