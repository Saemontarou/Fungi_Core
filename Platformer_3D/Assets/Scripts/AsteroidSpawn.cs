using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class AsteroidSpawn : MonoBehaviour
{
   public GameObject[] spawnObjects;
   
   private void Start()
   {
      StartCoroutine(Spawner());
   }
   
   IEnumerator Spawner()
   {
      while (true)
      {
         var objectIndex = Random.Range(0, spawnObjects.Length);
         yield return new WaitForSeconds(1);
         float rand = Random.Range(-6.5f, 6.5f);
         GameObject newAsteroid = Instantiate(spawnObjects[objectIndex], new Vector3(rand, 20, 0), Quaternion.identity);
      }
   }
}