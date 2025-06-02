using UnityEngine;

public class GateDoors : MonoBehaviour
{
   private Animator _animator;
   private AudioSource _audioSource;
   
   public void OnEnable()
   {
      ActionManager.OpenGate += OpenGates;
      ActionManager.CloseGate += CloseGates;
   }
   
   private void Start()
   {
      _animator = GetComponent<Animator>();
      _audioSource = GetComponent<AudioSource>();
   }
   
   private void OpenGates()
   {
      _animator.Play("GateDoorsOpen");
      _audioSource.Play();
   }
   
   private void CloseGates()
   {
      _animator.Play("GateDoorsClose");
   }
   
   public void OnDisable()
   {
      ActionManager.OpenGate -= OpenGates;
      ActionManager.CloseGate -= CloseGates;
   }
}