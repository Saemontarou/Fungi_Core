using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolObjectStones : MonoBehaviour
{
    [SerializeField] private GameObject _stonePrefab;

    [SerializeField] private AudioSource _audioThrow;
   
    public int poolSize = 10;

    private List<GameObject> _stonePool;
   
    private void Awake()
    {
        _stonePool = new List<GameObject>();
      
        for (int i = 0; i < poolSize; i++)
        {
            GameObject stone = Instantiate(_stonePrefab);
            stone.SetActive(false);
            _stonePool.Add(stone);
        }
    }

    public GameObject GetStoneFromPool()
    {
        foreach (GameObject stone in _stonePool)
        {
            if (!stone.activeInHierarchy)
            {
                stone.SetActive(true);
            
                _audioThrow.Play();
            
                return stone;
            }
        }
        return null;
    }

    public void ReturnStone(GameObject stone)
    {
        stone.SetActive(false);
    }
}