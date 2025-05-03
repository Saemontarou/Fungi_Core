using System;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public GameObject CanvasHealth;
    public Image BarLeft;
    public Image BarRight;
    
    public void UpdateBossHealthBar(int maxHealth, int currentHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        BarLeft.fillAmount = healthPercentage;
        BarRight.fillAmount = healthPercentage;
    }
}