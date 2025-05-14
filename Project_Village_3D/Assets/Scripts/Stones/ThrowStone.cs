using System;
using UnityEngine;

public class ThrowStone : MonoBehaviour
{
   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         ActionManager.TakeThrowStones();
      }
   }
}
