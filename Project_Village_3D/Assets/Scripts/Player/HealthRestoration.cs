using System;
using UnityEngine;

public class HealthRestoration : MonoBehaviour
{
    public int health;
    //public AudioSource crystalMagic;
    
    public void HealthZone()
    {
        PlayerHealth.Instance.TakeHealth(health);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating(nameof(HealthZone), 1, 1);
            //crystalMagic.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke(nameof(HealthZone));
        //crystalMagic.Stop();
    }
}