using System;
using UnityEngine;

public class GateDoors : MonoBehaviour
{
   private Animator animator;
   public AudioSource audioSource;

   //public static GateDoors Instance;

   public void OnEnable()
   {
      ActionManager.OpenGate += OpenGates;
      ActionManager.CloseGate += CloseGates;
   }
   
   private void Start()
   {
      //Instance = this;
      
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
   
   public void OnDisable()
   {
      ActionManager.OpenGate -= OpenGates;
      ActionManager.CloseGate -= CloseGates;
   }
}
