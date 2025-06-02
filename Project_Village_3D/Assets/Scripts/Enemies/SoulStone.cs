using UnityEngine;

public class SoulStone : MonoBehaviour
{
   public Animator upSoulStone;
   
   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag("Player"))
      {
         upSoulStone.Play("SpiritStoneUp");
      }
   }
}