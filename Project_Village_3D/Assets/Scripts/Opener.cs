using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opener : MonoBehaviour
{
    public Animator animator;

    public bool Opened = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Enter OKK");
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Opened == false)
                {
                    animator.SetBool("Opened", true);
                    Opened = true;
                }
                else if (Opened == true)
                {
                    animator.SetBool("Opened", false);
                    Opened = false;
                }
            }
        }
    }
}