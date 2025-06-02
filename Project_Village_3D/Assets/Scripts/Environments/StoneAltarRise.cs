using UnityEngine;

public class StoneAltarRise : MonoBehaviour
{
    public Animator animator;
    
    [SerializeField] private GameObject crystalManipulate;
    private CrystalManipulate _takeSoulKey;
    
    private void OnEnable()
    {
        ActionManager.AltarRiseAnimation += RiseAltar;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        if (crystalManipulate != null)
        {
            _takeSoulKey = crystalManipulate.GetComponent<CrystalManipulate>();
        }
    }
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _takeSoulKey.TakeSoulKey();
        }
    }
    
    private void RiseAltar()
    {
        animator.Play("StoneAltarRise");
    }

    private void OnDisable()
    {
        ActionManager.AltarRiseAnimation -= RiseAltar;
    }
}