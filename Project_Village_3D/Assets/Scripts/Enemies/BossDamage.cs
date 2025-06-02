using UnityEngine;
using System.Collections;

public class BossDamage : MonoBehaviour
{
    private int _damage = 50;
    public GameObject damageCanvas;
    public float displayDuration = 1f;
    
    [SerializeField] private GameObject playerHealth;
    private PlayerHealth _takeDamage;
    
    private void Start()
    {
        if (playerHealth != null)
        {
            _takeDamage = playerHealth.GetComponent<PlayerHealth>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _takeDamage.TakeDamage(_damage);
            ShowDamageEffect();
        }
    }
    
    public void ShowDamageEffect()
    {
        StartCoroutine(ShowDamageCoroutine());
    }

    private IEnumerator ShowDamageCoroutine()
    {
        damageCanvas.SetActive(true);
        yield return new WaitForSeconds(displayDuration);
        damageCanvas.SetActive(false);
    }
}