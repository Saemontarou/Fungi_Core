using System;
using UnityEngine;
using System.Collections;

public class Jumper : MonoBehaviour
{
    public GameObject Obj;
    public string Animator;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Obj.GetComponent<Animator>().Play(Animator);
        }
    }
}
