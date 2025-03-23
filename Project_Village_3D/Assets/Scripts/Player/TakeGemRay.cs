using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeGemRay : MonoBehaviour
{
    public float RayDistance = 2f;
    public Camera camera;
    public GameObject TakeButton;

    private bool Take;

    RaycastHit hit;

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
}