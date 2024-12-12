using System;
using UnityEngine;

public class DestroyBoundary : MonoBehaviour
{
   private void OnTriggerExit(Collider other)
   {
      Destroy(other.gameObject);
   }
}