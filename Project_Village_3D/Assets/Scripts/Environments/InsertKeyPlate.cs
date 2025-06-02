using UnityEngine;

public class InsertKeyPlate : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    
    [SerializeField] private GameObject crystalManipulate;
    private CrystalManipulate _insertSoulKey;

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        if (crystalManipulate != null)
        {
            _insertSoulKey = crystalManipulate.GetComponent<CrystalManipulate>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _insertSoulKey.InsertSoulKey();
        }
    }
}