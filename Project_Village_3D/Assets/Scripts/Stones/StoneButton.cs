using System;
using UnityEngine;

public class StoneButton : MonoBehaviour
{

    public Animator animator;
    public AudioSource audioSource;
    
    //public static StoneButton Instance;

    private void OnEnable()
    {
        ActionManager.PressStoneButton += PressButton;
    }

    private void Start()
    {
        //Instance = this;
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && PlayerCrystal.crystals >= 5)
        {
            CrystalManipulate.Instance.OpenGateAccess();
        }

        if (other.gameObject.CompareTag("Player") && PlayerCrystal.crystals <= 4)
        {
            CrystalManipulate.Instance.OpenGateDenied(); // Voice Denied
        }
    }

    public void PressButton()
    {
        animator.Play("ColumnButtonPress");
        //audioSource.Play();
    }
    
    private void OnDisable()
    {
        ActionManager.PressStoneButton -= PressButton;
    }
}