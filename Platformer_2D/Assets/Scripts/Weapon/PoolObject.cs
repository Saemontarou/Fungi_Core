using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _poolSize;
    private List<GameObject> _pool;

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject axe = Instantiate(_prefab);
            axe.SetActive(false);
            _pool.Add(axe);
        }
    }

    public GameObject GetObject()
    {
        foreach (var poolObj in _pool)
        {
            if (!poolObj.activeInHierarchy)
            {
                poolObj.SetActive(true);
                return poolObj;

            }
        }

        GameObject newAxe = Instantiate(_prefab);
        newAxe.SetActive(true);
        _pool.Add(newAxe);
        return newAxe;
    }
}
