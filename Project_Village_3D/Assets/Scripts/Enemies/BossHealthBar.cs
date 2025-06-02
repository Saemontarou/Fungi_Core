using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image barLeft;
    public Image barRight;
    
    public void UpdateBossHealthBar(int maxHealth, int currentHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        barLeft.fillAmount = healthPercentage;
        barRight.fillAmount = healthPercentage;
    }
}