using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Throw : MonoBehaviour
{
    [SerializeField] internal PoolObjectStones _poolObject;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _projectileSpeed = 20f;
    [SerializeField] private float _fireRate = 0.5f;
   
    //[SerializeField] private ParticleSystem _effect;
   
   
    internal int _currentAmmo;
    private float _lastThrowTime;
   
    public void ThrowStone()
    {
        if (CanThrow() && _currentAmmo > 0)
        {

            {
                //GameObject effect = Instantiate(_effect.gameObject, transform);
                //_effect.Play();
            
            }

            GameObject projectile = _poolObject.GetStoneFromPool();
            if (projectile)
            {
                projectile.transform.position = _spawnPoint.position;
                projectile.transform.rotation = _spawnPoint.rotation;
         
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                if (projectileScript)
                {
                    projectileScript.SetPool(_poolObject);
                }
         
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.linearVelocity = _spawnPoint.forward * _projectileSpeed;
                    _lastThrowTime = Time.time;
               
                }
            }

            _currentAmmo--;
        }
    }
   
    private bool CanThrow()
    {
        return Time.time - _lastThrowTime >= _fireRate;
    }
}