using System;
using UnityEngine;

public class ExitFence : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    //public static ExitFence Instance;

    private void OnEnable()
    {
        ActionManager.ExitFenceHide += FenceHide;
    }

    private void Start()
    {
        //Instance = this;
        
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }
    
    public void FenceHide()
    {
        animator.Play("FenceHide");
        //audioSource.Play();
    }

    private void OnDisable()
    {
        ActionManager.ExitFenceHide -= FenceHide;
    }
}
