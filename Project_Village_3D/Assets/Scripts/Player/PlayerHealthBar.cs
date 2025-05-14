using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public GameObject CanvasPlayerHealth;
    public Image BarLeft;
    public Image BarRight;
    
    public void UpdatePlayerHealthBar(int maxHealth, int currentHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        BarLeft.fillAmount = healthPercentage;
        BarRight.fillAmount = healthPercentage;
    }
}