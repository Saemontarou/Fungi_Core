using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Shoot : MonoBehaviour
{
   [SerializeField] private PoolObject _poolObject;
   [SerializeField] private Transform _spawnPoint;
   [SerializeField] private float _projectileSpeed = 20f;
   
   
   
   public void ShootBullet()
   {
      GameObject projectile = _poolObject.GetBulletFromPool();

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
         
      }
   }
}
