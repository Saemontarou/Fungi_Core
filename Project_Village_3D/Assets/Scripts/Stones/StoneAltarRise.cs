using System;
using UnityEngine;

public class StoneAltarRise : MonoBehaviour
{
    public Animator animator;
    //public AudioSource audioSource;

    //public static StoneAltarRise Instance;

    private void OnEnable()
    {
        ActionManager.AltarRiseAnimation += RiseAltar;
    }

    private void Start()
    {
        //Instance = this;
        
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CrystalManipulate.Instance.TakeSoulKey();
        }
    }
    
    public void RiseAltar()
    {
        animator.Play("StoneAltarRise");
        //audioSource.Play();
    }

    private void OnDisable()
    {
        ActionManager.AltarRiseAnimation -= RiseAltar;
    }
}