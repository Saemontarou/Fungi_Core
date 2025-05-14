using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class TowerBullet : MonoBehaviour 
{

    [SerializeField] private int damage = 10;
    [SerializeField] private float bulletSpeed = 5;
    private LayerMask layer;

    public void SetBullet(LayerMask layerMask, Vector3 direction)
    {
        layer = layerMask;
        Rigidbody body = GetComponent<Rigidbody>();
        body.useGravity = false;
        body.linearVelocity = direction * bulletSpeed;
        transform.forward = direction;
    }
	
    void OnTriggerEnter(Collider other)
    {
        if(!other.isTrigger)
        {
            if(((1 << other.gameObject.layer) & layer) != 0)
            {
                //other.GetComponent<BossHealth>().TakeDamage(damage);
                other.GetComponent<EnemyBoss>().TakeDamage(damage);
                //other.GetComponent<EnemyAI>().TakeDamage(damage);
            }
    
            Destroy(gameObject);
        }
    }
}