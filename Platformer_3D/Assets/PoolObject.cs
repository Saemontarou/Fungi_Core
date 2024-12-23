using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _poolSize;


    private List<GameObject> _bulletPool;

    private void Awake()
    {
        _bulletPool = new List<GameObject>();
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject bullet = Instantiate(_bulletPrefab);
            bullet.SetActive(false);
            _bulletPool.Add(bullet);
        }
    }

    public GameObject GetBullet(Vector3 SpawnPosition, Quaternion SpawnRotation)
    {
        foreach (var bullet in _bulletPool)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.SetActive(true);
                return bullet;
            }
        }

        GameObject newBullet = Instantiate(_bulletPrefab, SpawnPosition, SpawnRotation);
        newBullet.SetActive(true);
        _bulletPool.Add(newBullet);
        return newBullet;
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
    }
}