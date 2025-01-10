using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
   [SerializeField] private GameObject _bulletPrefab;
   public int poolSize = 20;

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
            return bullet;
         }
      }

      GameObject bulletObj = Instantiate(_bulletPrefab);
      bulletObj.SetActive(true);
      return bulletObj;
   }

   public void ReturnBullet(GameObject bullet)
   {
      bullet.SetActive(false);
   }
}
