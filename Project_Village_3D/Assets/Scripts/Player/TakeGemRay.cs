using UnityEngine;

public class TakeGemRay : MonoBehaviour
{
    public float RayDistance = 2f;
    public Camera camera;
    public GameObject TakeButton;

    public DeathZoneDamage zoneDamage;

    private bool Take;

    RaycastHit hit;

    public static TakeGemRay Instance;

    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, RayDistance, LayerMask.GetMask("Shard")) && Take == false)
        {
            TakeButton.active = true;

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.CompareTag("GemR") && Take != true)
                {
                    Take = true;
                    GrabRedShard();
                }

                if (hit.collider.gameObject.CompareTag("GemG") && Take != true)
                {
                    Take = true;
                    GrabGreenShard();
                }

                if (hit.collider.gameObject.CompareTag("GemB") && Take != true)
                {
                    Take = true;
                    GrabBlueShard();
                }

                if (hit.collider.gameObject.CompareTag("GemO") && Take != true)
                {
                    Take = true;
                    GrabOrangeShard();
                }

                if (hit.collider.gameObject.CompareTag("GemP") && Take != true)
                {
                    Take = true;
                    GrabPurpleShard();
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
            TakeButton.active = false;
        }

        Debug.DrawRay(ray.origin, ray.direction * 5, UnityEngine.Color.blue);
    }

    private void GrabRedShard()
    {
        Take = false;
        Debug.Log("YOU PICK RED");
        CrystalManager.Instance.TakeGemRed();
    }

    private void GrabGreenShard()
    {
        Take = false;
        Debug.Log("YOU PICK GREEN");
        CrystalManager.Instance.TakeGemGreen();
    }

    private void GrabBlueShard()
    {
        Take = false;
        Debug.Log("YOU PICK BLUE");
        CrystalManager.Instance.TakeGemBlue();

    }

    private void GrabOrangeShard()
    {
        Take = false;
        Debug.Log("YOU PICK ORANGE");
        CrystalManager.Instance.TakeGemOrange();
    }

    private void GrabPurpleShard()
    {
        Take = false;
        Debug.Log("YOU PICK PURPLE");
        CrystalManager.Instance.TakeGemPurple();
    }
    
    // public void TakeKeyFence()
    // {
    //     Take = false;
    //     Debug.Log("YOU TAKE KEY FENCE");
    //     CrystalManager.Instance.TakeKey();
    // }

    public void PutGems()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, RayDistance, LayerMask.GetMask("Altar")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    Take = true;
                    Debug.Log("YOU PUT ALL GEMS");
                    CrystalManager.Instance.PutAllGems(); //pullallgems invoke
                    StoneAltar.Instance.HideAltar(); 
                    GameManager.Instance.BossesVisible();
                    GateDoors.Instance.CloseGates();
                    StoneTurret.Instance.TurretRise();
                    zoneDamage.DamageZone();
                    
                    
                    //GameManager.Instance.TowerHeadVisible();
                }
            }
        }
    }

    public void OpenGate()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, RayDistance, LayerMask.GetMask("Button")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    Debug.Log("YOU PRESS BUTTON");
                    StoneButton.Instance.PressButton();
                    GateDoors.Instance.OpenGates();
                }
            }
        }
    }
    
    public void InsertKey()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, RayDistance, LayerMask.GetMask("Key")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    Debug.Log("YOU INSERT KEY AND OPEN FENCE");
                    CrystalManager.Instance._canvasKeyFence.SetActive(false);
                    ExitTrail.Instance.ExitTrailHide();
                    ExitFence.Instance.FenceHide();
                }
            }
        }
    }
    
    public void TakeKey()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, RayDistance, LayerMask.GetMask("Key")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    Debug.Log("YOU TAKE KEY FENCE");
                    CrystalManager.Instance.KeyHide();
                    ExitTrail.Instance.ExitTrailRise();
                    //GateDoors.Instance.OpenGates();
                }
            }
        }
    }
}