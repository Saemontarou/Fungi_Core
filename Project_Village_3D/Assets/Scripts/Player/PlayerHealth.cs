using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
    public int Maxhealth = 100;
    private int CurrentHealth;
    
    public AudioSource audioSource; // heartbeat

    public static PlayerHealth Instance; //
    
    public PlayerHealthBar PlayerHealthBar;
    
    private void Start()
    {
        Instance = this; //
        CurrentHealth = Maxhealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.Lose(); //
        }

        PlayerHealthBar.UpdatePlayerHealthBar(Maxhealth, CurrentHealth);
    }
}