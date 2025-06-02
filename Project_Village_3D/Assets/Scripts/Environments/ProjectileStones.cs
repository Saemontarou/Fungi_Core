using UnityEngine;

public class ProjectileStones : MonoBehaviour
{
    [SerializeField] private float _lifetime = 10f;
    private PoolObjectStones _poolObject;
    
    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), _lifetime);
    }

    private void OnDisable()
    {
        CancelInvoke(nameof(ReturnToPool));
    }

    public void SetPool(PoolObjectStones poolObject)
    {
        _poolObject = poolObject;
    }

    private void OnCollisionEnter (Collision other)
    {
        if (!other.gameObject.CompareTag("Player"))
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
        _poolObject.ReturnStone(gameObject);
    }
}