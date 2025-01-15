using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
   [SerializeField] private GameObject _bulletPrefab;

   [SerializeField] private AudioSource _audioShoot;
   
   public int poolSize = 32;

   private List<GameObject> _bulletPool;
   
   private void Awake()
   {
      _bulletPool = new List<GameObject>();
      
      for (int i = 0; i < poolSize; i++)
      {
         GameObject bullet = Instantiate(_bulletPrefab);
         bullet.SetActive(false);
         _bulletPool.Add(bullet);
      }
   }

   public GameObject GetBulletFromPool()
   {
      foreach (GameObject bullet in _bulletPool)
      {
         if (!bullet.activeInHierarchy)
         {
            bullet.SetActive(true);
            
            _audioShoot.Play();
            
            return bullet;
         }
      }

      // GameObject bulletObj = Instantiate(_bulletPrefab);
      // bulletObj.SetActive(true);
      // return bulletObj;
      return null;
   }

   public void ReturnBullet(GameObject bullet)
   {
      bullet.SetActive(false);
   }
}
