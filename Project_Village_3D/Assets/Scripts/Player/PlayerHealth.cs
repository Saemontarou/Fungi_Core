using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int _maxHealth = 1000;
    private int _currentHealth;
    
    public PlayerHealthBar PlayerHealthBar;
    
    [SerializeField] private GameObject gameManager;
    private GameManager _lose;
    
    public AudioSource healthDamage;
    public AudioSource healthRegeneration;
    
    private void Start()
    {
        _lose = gameManager.GetComponent<GameManager>();
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        healthDamage.Play();
        
        if (_currentHealth <= 0)
        {
            _lose.Lose();
            Destroy(gameObject);
        }

        PlayerHealthBar.UpdatePlayerHealthBar(_maxHealth, _currentHealth);
    }

    public void TakeHealth(int health)
    {
        if (_currentHealth < _maxHealth)
        {
            _currentHealth += health;
            healthRegeneration.Play();
            if (_currentHealth > _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
        
        PlayerHealthBar.UpdatePlayerHealthBar(_maxHealth, _currentHealth);
    }
}