using System;
using UnityEngine;

public class DestroyBoundary : MonoBehaviour
{
   [SerializeField] private PoolObject _poolObject;
   
   private void OnTriggerExit(Collider other)
   {
      if(other.CompareTag("Bolt"))
      {
         _poolObject.ReturnBullet(other.gameObject);
         return;
      }
      Destroy(other.gameObject);
   }
}