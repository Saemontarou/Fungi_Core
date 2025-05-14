using System;
using Unity.VisualScripting;
using UnityEngine;

public class StoneAltar : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public Collider stonePlate;

    //public static StoneAltar Instance;

    private void OnEnable()
    {
        ActionManager.AltarHideAnimation += HideAltar;
    }

    private void Start()
    {
        //Instance = this;
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        stonePlate = gameObject.GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CrystalManipulate.Instance.PutGems();
        }
    }

    public void HideAltar()
    {
        animator.Play("StoneAltarHide");
        audioSource.Play();
        stonePlate.enabled = false;
        Destroy(gameObject, 15f);
    }

    private void OnDisable()
    {
        ActionManager.AltarHideAnimation -= HideAltar;
    }
}