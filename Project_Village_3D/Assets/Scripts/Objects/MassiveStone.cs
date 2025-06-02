using UnityEngine;

public class MassiveStone : MonoBehaviour
{
   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         ActionManager.TakeThrowStones();
      }
   }
}