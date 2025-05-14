using UnityEngine;

public class InsertKeyPlate : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;

    public static InsertKeyPlate Instance;


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
            CrystalManipulate.Instance.InsertSoulKey();
        }
    }
}
    