using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    public Image barLeft;
    public Image barRight;
    
    public void UpdatePlayerHealthBar(int maxHealth, int currentHealth)
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        barLeft.fillAmount = healthPercentage;
        barRight.fillAmount = healthPercentage;
    }
}