using UnityEngine;

public class StoneAltar : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public static StoneAltar Instance;

    
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
            TakeGemRay.Instance.PutGems();
        }
    }

    public void HideAltar()
    {
        animator.Play("StoneAltarHide");
        audioSource.Play();
        Destroy(gameObject, 15f);
    }
}