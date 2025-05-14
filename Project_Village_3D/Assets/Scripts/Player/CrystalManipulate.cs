using System;
using UnityEngine;

public class CrystalManipulate : MonoBehaviour
{
    public float rayDistance = 2f;
    public Camera _camera;
    public GameObject takeButton;

    //public DeathZoneDamage zoneDamage;
    
    private bool Take;

    RaycastHit hit;

    [SerializeField] private AudioSource openDenied;

    public static CrystalManipulate Instance;

    private void OnEnable()
    {
        ActionManager.TakeShards += TakeShard;
    }

    void Start()
    {
        Instance = this;
    }

    void TakeShard()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Shard")) && Take == false)
        {
            takeButton.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.CompareTag("GemR") && Take != true)
                {
                    Take = true;
                    ActionManager.RedCrystalShardGrab();
                    //GrabRedShard();
                    {
                        Take = false;
                    }
                }

                if (hit.collider.gameObject.CompareTag("GemG") && Take != true)
                {
                    Take = true;
                    ActionManager.GreenCrystalShardGrab();
                    //GrabGreenShard();
                    {
                        Take = false;
                    }
                    
                }

                if (hit.collider.gameObject.CompareTag("GemB") && Take != true)
                {
                    Take = true;
                    ActionManager.BlueCrystalShardGrab();
                    //GrabBlueShard();
                    {
                        Take = false;
                    }
                }

                if (hit.collider.gameObject.CompareTag("GemO") && Take != true)
                {
                    Take = true;
                    ActionManager.OrangeCrystalShardGrab();
                    //GrabOrangeShard();
                    {
                        Take = false;
                    }
                }

                if (hit.collider.gameObject.CompareTag("GemP") && Take != true)
                {
                    Take = true;
                    ActionManager.PurpleCrystalShardGrab();
                    //GrabPurpleShard();
                    {
                        Take = false;
                    }
                }
                
                // if (hit.collider.gameObject.CompareTag("Key") && Take != true)
                // {
                //     Take = true;
                //     TakeKeyFence();
                // }
            }
        }
        else
        {
            takeButton.SetActive(false);
        }

        Debug.DrawRay(ray.origin, ray.direction * 5, UnityEngine.Color.blue);
    }

    // private void GrabRedShard()
    // {
    //     Take = false;
    //     Debug.Log("YOU PICK RED");
    //     CrystalManager.Instance.TakeGemRed();
    // }
    //
    // private void GrabGreenShard()
    // {
    //     Take = false;
    //     Debug.Log("YOU PICK GREEN");
    //     CrystalManager.Instance.TakeGemGreen();
    // }
    //
    // private void GrabBlueShard()
    // {
    //     Take = false;
    //     Debug.Log("YOU PICK BLUE");
    //     CrystalManager.Instance.TakeGemBlue();
    //
    // }
    //
    // private void GrabOrangeShard()
    // {
    //     Take = false;
    //     Debug.Log("YOU PICK ORANGE");
    //     CrystalManager.Instance.TakeGemOrange();
    // }
    //
    // private void GrabPurpleShard()
    // {
    //     Take = false;
    //     Debug.Log("YOU PICK PURPLE");
    //     CrystalManager.Instance.TakeGemPurple();
    // }
    
    // public void TakeKeyFence()
    // {
    //     Take = false;
    //     Debug.Log("YOU TAKE KEY FENCE");
    //     CrystalManager.Instance.TakeKey();
    // }

    public void PutGems()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Altar")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                ActionManager.PutGemsOnStone();
                Debug.Log("YOU PUT ALL GEMS");
                

                //CrystalManager.Instance.PutAllGems();
                //StoneAltar.Instance.HideAltar(); 
                //GameManager.Instance.BossesVisible();
                //GateDoors.Instance.CloseGates();
                //StoneTurret.Instance.TurretRise();
                //zoneDamage.DamageZone();
                //GameManager.Instance.TowerHeadVisible();
            }
        }
    }

    public void OpenGateAccess()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Button")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    ActionManager.OpenMainGate();
                    Debug.Log("YOU PRESS BUTTON");
                    
                    //StoneButton.Instance.PressButton();
                    //GateDoors.Instance.OpenGates();
                }
            }
        }
        
        Debug.DrawRay(ray.origin, ray.direction * 5, UnityEngine.Color.magenta);
    }
    public void OpenGateDenied()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Button")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    Debug.Log("YOU MUST FIND ALL CRYSTALS SHARD");
                    openDenied.Play();
                    
                    
                    //StoneButton.Instance.PressButton();
                    //GateDoors.Instance.OpenGates();
                }
            }
        }
        
        Debug.DrawRay(ray.origin, ray.direction * 5, UnityEngine.Color.magenta);
    }
    
    public void InsertSoulKey()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Key")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    ActionManager.InsertKeyOnHole();
                    Debug.Log("YOU INSERT KEY AND OPEN FENCE");
                    
                    //CrystalManager.Instance._canvasKeyFence.SetActive(false);
                    //ExitTrail.Instance.ExitTrailHide();
                    //ExitFence.Instance.FenceHide();
                }
            }
        }
    }
    
    public void TakeSoulKey()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Key")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    ActionManager.TakeKeyOnStone();
                    Debug.Log("YOU TAKE KEY FENCE");
                    
                    //CrystalManager.Instance.KeyHide();
                    //ExitTrail.Instance.ExitTrailRise();
                    //GateDoors.Instance.OpenGates();
                }
            }
        }
    }

    private void OnDisable()
    {
        ActionManager.TakeShards -= TakeShard;
    }
}