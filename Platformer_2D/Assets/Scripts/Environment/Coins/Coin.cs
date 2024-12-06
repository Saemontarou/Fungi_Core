using System;
using UnityEngine;

public class Coin : MonoBehaviour

{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        { 
            MoneyText.Coin += 1;
            Destroy(gameObject);
        }
    }
}