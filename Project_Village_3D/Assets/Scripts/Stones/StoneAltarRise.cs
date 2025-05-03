using UnityEngine;

public class StoneAltarRise : MonoBehaviour
{
    public Animator animator;
    //public AudioSource audioSource;

    public static StoneAltarRise Instance;

    
    private void Start()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        //audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            TakeGemRay.Instance.TakeKey();
        }
    }
    
    public void RiseAltar()
    {
        animator.Play("StoneAltarRise");
        //audioSource.Play();
    }

    
}