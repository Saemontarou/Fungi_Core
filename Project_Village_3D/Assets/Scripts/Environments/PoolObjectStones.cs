using System.Collections.Generic;
using UnityEngine;

public class PoolObjectStones : MonoBehaviour
{
    private List<GameObject> _stonePool;
    public int poolSize = 10;
    
    [SerializeField] private GameObject _stonePrefab;
    [SerializeField] private AudioSource _audioThrow;
    
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