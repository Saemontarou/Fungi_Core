using System;
using UnityEngine;

public class BossDamageHand : MonoBehaviour
{

    [SerializeField] private int damage = 50;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("RIGHT HAND ATTACK");
            PlayerHealth.Instance.TakeDamage(damage);
        }
    }
}
