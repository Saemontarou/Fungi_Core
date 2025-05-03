using UnityEngine;


public class BossHealth : MonoBehaviour 
{
    public int Maxhealth = 100;
    private int CurrentHealth;

    public BossHealthBar BossHealthBar;
    
    private void Start()
    {
        CurrentHealth = Maxhealth;
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0)
        {
            Destroy(gameObject);
            StoneTurret.Instance.TurretHide();
            StoneAltarRise.Instance.RiseAltar();
        }
        BossHealthBar.UpdateBossHealthBar(Maxhealth, CurrentHealth);
    }
}