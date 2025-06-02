using UnityEngine;

public class StoneButton : MonoBehaviour
{
    public Animator animator;
    
    [SerializeField] private GameObject crystalManipulate;
    private CrystalManipulate _openGateAccess;
    private CrystalManipulate _openGateDenied;
    
    private void OnEnable()
    {
        ActionManager.PressStoneButton += PressButton;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        if (crystalManipulate != null)
        {
            _openGateAccess = crystalManipulate.GetComponent<CrystalManipulate>();
            _openGateDenied = crystalManipulate.GetComponent<CrystalManipulate>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && CrystalScore.Crystals >= 5)
        {
            _openGateAccess.OpenGateAccess();
        }
        
        if (other.gameObject.CompareTag("Player") && CrystalScore.Crystals <= 4)
        {
            _openGateDenied.OpenGateDenied();
        }
    }

    private void PressButton()
    {
        animator.Play("ColumnButtonPress");
    }
    
    private void OnDisable()
    {
        ActionManager.PressStoneButton -= PressButton;
    }
}