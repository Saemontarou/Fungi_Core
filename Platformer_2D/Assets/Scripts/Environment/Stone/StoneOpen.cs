using System;
using UnityEngine;

public class StoneOpen : MonoBehaviour
{
    
    private void OnEnable()
    {
        LeverOpen.OnStoneOpen += IsStoneOpen;
    }

    private void OnDisable()
    {
        LeverOpen.OnStoneOpen -= IsStoneOpen;
    }
   
    private void IsStoneOpen()
    {
        gameObject.SetActive(false);
    }
}