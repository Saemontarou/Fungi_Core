using System;
using Unity.VisualScripting;
using UnityEngine;

public class ProjectileCore : MonoBehaviour
{
    [SerializeField] private float _lifetime = 10f;
    private PoolObjectCores _poolObject;
    
    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), _lifetime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ReturnToPool));
    }

    public void SetPool(PoolObjectCores poolObject)
    {
        _poolObject = poolObject;
    }

    private void OnCollisionEnter (Collision other)
    {
        if (!other.rigidbody.CompareTag("Turret"))
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0)
            {
                ReturnToPool();
            }
        }
    }
    
    private void ReturnToPool()
    {
        _poolObject.ReturnCore(gameObject);
    }
}