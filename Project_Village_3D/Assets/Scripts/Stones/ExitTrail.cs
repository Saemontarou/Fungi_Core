using System;
using UnityEngine;

public class ExitTrail : MonoBehaviour
{

    public Animator animator;
    public AudioSource audioSource;
    
    //public static ExitTrail Instance;

    private void OnEnable()
    {
        ActionManager.ExitKeyHoleRise += ExitTrailRise;
        ActionManager.ExitKeyHoleHide += ExitTrailHide;
    }

    private void Start()
    {
        //Instance = this;
        
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }
    

    public void ExitTrailRise()
    {
        animator.Play("ExitTrailRise");
        //audioSource.Play();
    }

    public void ExitTrailHide()
    {
        animator.Play("ExitTrailHide");
        //audioSource.Play();
    }

    private void OnDisable()
    {
        ActionManager.ExitKeyHoleRise -= ExitTrailRise;
        ActionManager.ExitKeyHoleHide -= ExitTrailHide;
    }
}