using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PoolObjectCores : MonoBehaviour
{
    [SerializeField] private GameObject _corePrefab;

    [SerializeField] private AudioSource _audioThrow;
   
    public int poolSize = 5;

    private List<GameObject> _corePool;
   
    private void Awake()
    {
        _corePool = new List<GameObject>();
      
        for (int i = 0; i < poolSize; i++)
        {
            GameObject core = Instantiate(_corePrefab);
            core.SetActive(false);
            _corePool.Add(core);
        }
    }

    public GameObject GetCoreFromPool()
    {
        foreach (GameObject core in _corePool)
        {
            if (!core.activeInHierarchy)
            {
                core.SetActive(true);
            
                _audioThrow.Play();
            
                return core;
            }
        }
        return null;
    }

    public void ReturnCore(GameObject core)
    {
        core.SetActive(false);
    }
}