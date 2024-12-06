using System;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
 
   private void OnEnable()
   {
      OpenKey.OnDoorOpen += IsDoorOpen;
   }

   private void OnDisable()
   {
      OpenKey.OnDoorOpen -= IsDoorOpen;
   }
   
   private void IsDoorOpen()
   {
      gameObject.SetActive(false);
   }
}