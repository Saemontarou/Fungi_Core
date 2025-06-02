using UnityEngine;

public class CrystalShard : MonoBehaviour
{
   private void OnTriggerStay(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         ActionManager.TakeCrystalShard();
      }
   }
}