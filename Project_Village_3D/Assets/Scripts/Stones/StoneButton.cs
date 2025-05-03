using System;
using UnityEngine;

public class StoneButton : MonoBehaviour
{

    public Animator animator;
    public AudioSource audioSource;
    
    public static StoneButton Instance;
    
    private void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeGemRay.Instance.OpenGate();
        }
    }

    public void PressButton()
    {
        animator.Play("ColumnButtonPress");
        //audioSource.Play();
    }
}