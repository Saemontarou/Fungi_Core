using UnityEngine;

public class ExitFence : MonoBehaviour
{
    public Animator animator;

    private void OnEnable()
    {
        ActionManager.ExitFenceHide += FenceHide;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    private void FenceHide()
    {
        animator.Play("FenceHide");
    }

    private void OnDisable()
    {
        ActionManager.ExitFenceHide -= FenceHide;
    }
}