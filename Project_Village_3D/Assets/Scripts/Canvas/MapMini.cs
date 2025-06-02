using UnityEngine;

public class MapMini : MonoBehaviour
{
   [SerializeField] private Transform player;

   private void LateUpdate()
   {
      if (player == null)
         return;
      
      Vector3 newPosition = player.position;
      newPosition.y = transform.position.y;
      transform.position = newPosition;
      transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
   }
}