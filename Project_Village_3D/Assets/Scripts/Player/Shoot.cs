using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Shoot : MonoBehaviour
{
    [SerializeField] internal PoolObjectCores _poolObject;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _projectileSpeed = 20f;
    [SerializeField] private float _fireRate = 0.5f;
   
    //[SerializeField] private ParticleSystem _effect;
   
   
    //internal int _currentAmmo;
    private float _lastShootTime;
   
    public void ShootCore()
    {
        if (CanShoot()) //&& _currentAmmo > 0)
        {

            {
                //GameObject effect = Instantiate(_effect.gameObject, transform);
                //_effect.Play();
            
            }

            GameObject projectile = _poolObject.GetCoreFromPool();
            if (projectile)
            {
                projectile.transform.position = _spawnPoint.position;
                projectile.transform.rotation = _spawnPoint.rotation;
         
                ProjectileCore projectileScript = projectile.GetComponent<ProjectileCore>();
                if (projectileScript)
                {
                    projectileScript.SetPool(_poolObject);
                }
         
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.linearVelocity = _spawnPoint.forward * _projectileSpeed;
                    _lastShootTime = Time.time;
               
                }
            }

            //_currentAmmo--;
        }
    }
   
    private bool CanShoot()
    {
        return Time.time - _lastShootTime >= _fireRate;
    }
}