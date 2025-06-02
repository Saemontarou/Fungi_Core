using UnityEngine;

public class ExitTrail : MonoBehaviour
{
    public Animator animator;
    
    private void OnEnable()
    {
        ActionManager.ExitKeyHoleRise += ExitTrailRise;
        ActionManager.ExitKeyHoleHide += ExitTrailHide;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void ExitTrailRise()
    {
        animator.Play("ExitTrailRise");
    }

    private void ExitTrailHide()
    {
        animator.Play("ExitTrailHide");
    }

    private void OnDisable()
    {
        ActionManager.ExitKeyHoleRise -= ExitTrailRise;
        ActionManager.ExitKeyHoleHide -= ExitTrailHide;
    }
}