using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public static int health = 6;

    public int numberLives;

    public Image[] lives;
    
    public Sprite fullLive;
    public Sprite halfLive;
    public Sprite emptyLive;

    private void Start()
    {
        
    }

    private void Update()
    {
        
        for (int i = 0; i < lives.Length; i++)
        {
            if(health > numberLives * 2)
            {
                health = numberLives * 2;
            }    
            
            
            if (i * 2 < health - 1)
            {
                lives[i].sprite = fullLive;
            }
            else if (i * 2 == health - 1)
            {
                lives[i].sprite = halfLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }
            
            
            if (i < numberLives)
            {
                lives[i].enabled = true;
            }
            else
            {
                lives[i].enabled = false;
            }
        }
    }
}