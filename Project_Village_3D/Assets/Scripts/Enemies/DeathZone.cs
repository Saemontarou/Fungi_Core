using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private int _damage = 10;
    [SerializeField] private GameObject playerHealth;
    private PlayerHealth _takeDamage;
    
    public AudioSource heartBeat;
    
    private void Start()
    {
        if (playerHealth != null)
        {
            _takeDamage = playerHealth.GetComponent<PlayerHealth>();
        }
    }
    
    public void DamageZone()
    {
        if (_takeDamage != null)
        {
            _takeDamage.TakeDamage(_damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerHealth != null)
            {
                _takeDamage = playerHealth.GetComponent<PlayerHealth>();
            }
            
            InvokeRepeating(nameof(DamageZone), 1f, 3f);
            heartBeat.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke(nameof(DamageZone));
        heartBeat.Stop();
    }
}