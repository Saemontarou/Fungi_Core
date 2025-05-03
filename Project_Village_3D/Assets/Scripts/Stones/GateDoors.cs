using System;
using UnityEngine;

public class GateDoors : MonoBehaviour
{
   private Animator animator;
   public AudioSource audioSource;

   public static GateDoors Instance;


   private void Start()
   {
      Instance = this;
      animator = GetComponent<Animator>();
      audioSource = GetComponent<AudioSource>();
   }
   
   public void OpenGates()
   {
      animator.Play("GateDoorsOpen");
      audioSource.Play();
   }
   
   public void CloseGates()
   {
      animator.Play("GateDoorsClose");
   }
}
