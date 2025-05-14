using System;
using UnityEngine;

public class StoneTurret : MonoBehaviour
{
    private Animator animator;
    public AudioSource audioSource;
    
    [Header("Башни с кристаллами")] 
    public GameObject towerRedHead;
    public GameObject towerGreenHead;
    public GameObject towerBlueHead;
    public GameObject towerOrangeHead;
    public GameObject towerPurpleHead;

    //public static StoneTurret Instance;

    private void OnEnable()
    {
        ActionManager.TurretRiseAnimation += TurretRise;
        ActionManager.TurretHideAnimation += TurretHide;
        //ActionManager.TurretHeadVisible += TowerHeadVisible;
    }


    private void Start()
    {
        //Instance = this;
        
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
   
    public void TurretRise()
    {
        animator.Play("StoneTowerRise");
        //audioSource.Play();
    }
   
    public void TurretHide()
    {
        animator.Play("StoneTowerHide");
        //audioSource.Play();
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
        //ActionManager.TurretHeadVisible -= TowerHeadVisible;
    }
}
