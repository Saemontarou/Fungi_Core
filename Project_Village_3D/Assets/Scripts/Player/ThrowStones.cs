using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class ThrowStones : MonoBehaviour
{
    [SerializeField] internal PoolObjectStones _poolObject;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private float _projectileSpeed = 20f;
    [SerializeField] private float _fireRate = 0.5f;
    
    internal int _currentStones;
    private float _lastThrowTime;
   
    public void ThrowStone()
    {
        if (CanThrow() && _currentStones > 0)
        {
            GameObject stone = _poolObject.GetStoneFromPool();
            if (stone)
            {
                stone.transform.position = _spawnPoint.position;
                stone.transform.rotation = _spawnPoint.rotation;
         
                ProjectileStones projectileScript = stone.GetComponent<ProjectileStones>();
                if (projectileScript)
                {
                    projectileScript.SetPool(_poolObject);
                }
         
                Rigidbody rb = stone.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.linearVelocity = _spawnPoint.forward * _projectileSpeed;
                    _lastThrowTime = Time.time;
                }
            }
            
            _currentStones--;
        }
    }
   
    private bool CanThrow()
    {
        return Time.time - _lastThrowTime >= _fireRate;
    }
}