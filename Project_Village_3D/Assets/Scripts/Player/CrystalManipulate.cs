using UnityEngine;

public class CrystalManipulate : MonoBehaviour
{
    public float rayDistance = 2f;
    public Camera playerCamera;
    public GameObject takeButton;
    
    [SerializeField] private AudioSource openDenied;
    
    private bool Take;

    RaycastHit hit;
    
    private void Update()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Shard")) && Take == false)
        {
            takeButton.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.collider.gameObject.CompareTag("GemR") && Take != true)
                {
                    Take = true;
                    ActionManager.RedCrystalShardGrab();
                    {
                        Take = false;
                    }
                }

                if (hit.collider.gameObject.CompareTag("GemG") && Take != true)
                {
                    Take = true;
                    ActionManager.GreenCrystalShardGrab();
                    {
                        Take = false;
                    }
                    
                }

                if (hit.collider.gameObject.CompareTag("GemB") && Take != true)
                {
                    Take = true;
                    ActionManager.BlueCrystalShardGrab();
                    {
                        Take = false;
                    }
                }

                if (hit.collider.gameObject.CompareTag("GemO") && Take != true)
                {
                    Take = true;
                    ActionManager.OrangeCrystalShardGrab();
                    {
                        Take = false;
                    }
                }

                if (hit.collider.gameObject.CompareTag("GemP") && Take != true)
                {
                    Take = true;
                    ActionManager.PurpleCrystalShardGrab();
                    {
                        Take = false;
                    }
                }
            }
        }
        
        else
        {
            takeButton.SetActive(false);
        }

        //Debug.DrawRay(ray.origin, ray.direction * 5, UnityEngine.Color.blue);
    }

    public void PutGems()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Altar")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                ActionManager.PutGemsOnStone();
            }
        }
    }

    public void OpenGateAccess()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Button")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    ActionManager.OpenMainGate();
                }
            }
        }
    }
    
    public void OpenGateDenied()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Button")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    openDenied.Play();
                }
            }
        }
    }
    
    public void InsertSoulKey()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Key")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    ActionManager.InsertKeyOnHole();
                }
            }
        }
    }
    
    public void TakeSoulKey()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit, rayDistance, LayerMask.GetMask("Key")))
        {
            if (Input.GetMouseButtonDown(0))
            {
                {
                    ActionManager.TakeKeyOnStone();
                }
            }
        }
    }
}