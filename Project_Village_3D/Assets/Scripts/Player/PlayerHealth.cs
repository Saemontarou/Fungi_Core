using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour 
{
    public int Maxhealth = 100; //
    public int CurrentHealth;
    
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

    public void TakeHealth(int health)
    {
        //CurrentHealth += health;
        if (CurrentHealth < Maxhealth);
        {
            CurrentHealth += health;
            if (CurrentHealth > Maxhealth)
            {
                CurrentHealth = Maxhealth;
                Debug.Log("OKAY, YOUR HEALTH FULL");
            }
        }

        PlayerHealthBar.UpdatePlayerHealthBar(Maxhealth, CurrentHealth);
    }
}