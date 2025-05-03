using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public GameObject CanvasPlayerHealth;
    public Image Bar;
    
    public void UpdatePlayerHealthBar(int maxHealth, int currentHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        Bar.fillAmount = healthPercentage;
    }
}