using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
   // public Slider HealthSlider;
   // public Transform Parent;
   // public Vector3 Offset = new Vector3(0, 2, 0);
   //
   // private int _maxHealth = 100;
   // private int _currentHealth;
   //
   // private void Start()
   // {
   //    _currentHealth = _maxHealth;
   //    HealthSlider.maxValue = _maxHealth;
   //    HealthSlider.value = _currentHealth;
   // }
   //
   // private void Update()
   // {
   //    if (Parent)
   //    {
   //       HealthSlider.transform.position = Parent.position + Offset;
   //    }
   // }
   //
   // public void TakeDamage(int damage)
   // {
   //    _currentHealth -= damage;
   //    HealthSlider.value = _currentHealth;
   //    if (_currentHealth <= 0)
   //    {
   //       Die();
   //    }
   // }
   //
   // private void Die()
   // {
   //    Destroy(gameObject);
   // }


   public Transform _healthBarAnhor;
   public HealthBarPool Pool;
   private GameObject _healthBar;
   private Slider _healthSlider;

   private int _maxHealth;
   private int _currentHealth;

   private void Start()
   {
      _healthBar = Pool.GetHealthBar();

      _healthBar.transform.position = _healthBarAnhor.position;
      _healthSlider = _healthBar.GetComponentInChildren<Slider>();
      _healthSlider.maxValue = _maxHealth;
      _currentHealth = _maxHealth;
   }

   private void Update()
   {
      if (_healthBar != null)
      {
         _healthBar.transform.position = _healthBarAnhor.position;
      }
   }

   public void TakeDamge(int damage)
   {
      _currentHealth -= damage;
      _healthSlider.value = _currentHealth;
      if (_currentHealth <= 0)
      {
         Die();
      }
   }

   private void OnCollisionEnter(Collision other)
   {
      if (other.gameObject.CompareTag("Bullet"))
      {
         TakeDamge(20);
      }
   }

   private void Die()
   {
      Pool.ReturnHealthBar(_healthBar);
      Destroy(gameObject);
   }
}

public class HealthBarPool : MonoBehaviour
{
   [SerializeField] private GameObject _healthBarPrefab;
   [SerializeField] private int _poolSize;
   private Queue<GameObject> _pool;

   private void Start()
   {
      _pool = new Queue<GameObject>();

      for (int i = 0; i < _poolSize; i++)
      {
         GameObject healthBar = Instantiate(_healthBarPrefab, transform);
         healthBar.SetActive(false);
         _pool.Enqueue(healthBar);
      }
   }

   public GameObject GetHealthBar()
   {
      if (_pool.Count > 0)
      {
         GameObject healthBar = _pool.Dequeue();
         healthBar.SetActive(true);
         return healthBar;
      }
      else
      {
         GameObject healthBar = Instantiate(_healthBarPrefab, transform);
         return healthBar;
      }
   }

   public void ReturnHealthBar(GameObject healthBar)
   {
      healthBar.SetActive(false);
      _pool.Enqueue(healthBar);
   }
}

public class HealthBarLookAt : MonoBehaviour
{
   private Camera _camera;


   private void Start()
   {
      _camera = Camera.main;
   }


   private void LateUpdate()
   {
      if (_camera != null)
      {
         transform.LookAt(_camera.transform);
         transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + 180f, 0f);
      }
   }
}
