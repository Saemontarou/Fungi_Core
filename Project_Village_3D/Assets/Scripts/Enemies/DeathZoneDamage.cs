using System;
using UnityEngine;

public class DeathZoneDamage : MonoBehaviour
{
    public int damage;
    public AudioSource heartBeat;
    
    public void DamageZone()
    {
        PlayerHealth.Instance.TakeDamage(damage);
        heartBeat.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating(nameof(DamageZone), 1, 2);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke(nameof(DamageZone));
        heartBeat.Stop();
    }
}