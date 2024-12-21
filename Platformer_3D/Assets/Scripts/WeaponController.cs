using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
   [SerializeField] private GameObject _shotPrefab;
   [SerializeField] private Transform _shotSpawn;
   [SerializeField] private float _fireRate;
   [SerializeField] private float _initialDelay;

   private AudioSource _audioSource;

   private void Start()
   {
      _audioSource = GetComponent<AudioSource>();
      InvokeRepeating("Fire", _initialDelay, _fireRate);
   }

   void Fire()
   {
      Instantiate(_shotPrefab, _shotSpawn.position, _shotSpawn.rotation);
      _audioSource.Play();
   }
}