using System;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   [SerializeField] private float _lifetime = 5f;
   private PoolObject _poolObject;

   private void OnEnable()
   {
      Invoke(nameof(ReturnToPool), _lifetime);
   }

   private void OnDisable()
   {
      CancelInvoke(nameof(ReturnToPool));
   }

   public void SetPool(PoolObject poolObject)
   {
      _poolObject = poolObject;
   }

   private void OnCollisionEnter(Collision other)
   {
      if (!other.gameObject.CompareTag("Player"))
      {
         ReturnToPool();
      }
   }

   private void ReturnToPool()
   {
      _poolObject.ReturnBullet(gameObject);
   }
}
