using System;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _playerExplosion;
    [SerializeField] private int _scoreValue;
    [SerializeField] private GameManager _gameManager;
    
    private void OnTriggerEnter(Collider other)
    {
        if (
            (CompareTag("Bolt") && other.CompareTag("Player"))
            || (CompareTag("EnemyBolt") && other.CompareTag("Enemy"))
            || other.CompareTag("Boundary")
           )
        {
            return;
        }
        
        if (_explosion != null && !CompareTag("Bolt"))
        {
            Instantiate(_explosion, transform.position, transform.rotation);
        }

        if (other.CompareTag("Player"))
        {
            Instantiate(_playerExplosion, other.transform.position, other.transform.rotation);
        }

        _gameManager.AddScore(_scoreValue);
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}