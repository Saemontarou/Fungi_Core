using UnityEngine;

public class StoneAltarHide : MonoBehaviour
{
    public Animator animator;
    public AudioSource audioSource;
    public Collider stonePlate;
    
    [SerializeField] private GameObject crystalManipulate;
    private CrystalManipulate _putGems;
    
    private void OnEnable()
    {
        ActionManager.AltarHideAnimation += HideAltar;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        stonePlate = gameObject.GetComponent<Collider>();
        
        if (crystalManipulate != null)
        {
            _putGems = crystalManipulate.GetComponent<CrystalManipulate>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _putGems.PutGems();
        }
    }

    private void HideAltar()
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