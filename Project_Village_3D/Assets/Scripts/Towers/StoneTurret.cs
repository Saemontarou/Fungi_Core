using UnityEngine;

public class StoneTurret : MonoBehaviour
{
    private Animator animator;
    
    [Header("Башни с кристаллами")] 
    public GameObject towerRedHead;
    public GameObject towerGreenHead;
    public GameObject towerBlueHead;
    public GameObject towerOrangeHead;
    public GameObject towerPurpleHead;
    
    private void OnEnable()
    {
        ActionManager.TurretRiseAnimation += TurretRise;
        ActionManager.TurretHideAnimation += TurretHide;
    }
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
   
    private void TurretRise()
    {
        animator.Play("StoneTowerRise");
    }
   
    private void TurretHide()
    {
        animator.Play("StoneTowerHide");
    }
    
    public void TowerHeadVisible()
    {
        towerRedHead.SetActive(true);
        towerGreenHead.SetActive(true);
        towerBlueHead.SetActive(true);
        towerOrangeHead.SetActive(true);
        towerPurpleHead.SetActive(true);
    }

    private void OnDisable()
    {
        ActionManager.TurretRiseAnimation -= TurretRise;
        ActionManager.TurretHideAnimation -= TurretHide;
    }
}