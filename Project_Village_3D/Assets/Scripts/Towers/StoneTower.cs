using UnityEngine;

[RequireComponent(typeof(SphereCollider))]

public class StoneTower : MonoBehaviour 
{
	[SerializeField] private TowerBullet bulletPrefab;
	[SerializeField] private float fireRate = 1;
	[SerializeField] private float smooth = 1;
	[SerializeField] private float rayOffset = 1;
	[SerializeField] private float damage;
	[SerializeField] private Transform bulletPoint;
	[SerializeField] private Transform stoneHeadRotation;
	[SerializeField] private Transform center;
	[SerializeField] private LayerMask layerMask;
	
	private SphereCollider turretTrigger;
	private Transform target;
	private Vector3 offset;
	private int index;
	private float curFireRate;
	private Quaternion defaultRot = Quaternion.identity;

	private void Awake()
	{
		turretTrigger = GetComponent<SphereCollider>();
		turretTrigger.isTrigger = true;
		offset = turretTrigger.center;
		curFireRate = fireRate;
		turretTrigger.enabled = true;
		enabled = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		if(CheckLayerMask(other.gameObject, layerMask))
		{
			target = other.transform;
			turretTrigger.enabled = false;
			enabled = true;
		}
	}

	private Transform FindTarget()
	{
		Collider[] colliders = Physics.OverlapSphere(transform.position + offset, turretTrigger.radius, layerMask);
		Collider currentCollider = null;
		float dist = Mathf.Infinity;
		
		foreach(Collider coll in colliders)
		{
			float currentDist = Vector3.Distance(transform.position + offset, coll.transform.position);
			
			if(currentDist < dist)
			{
				currentCollider = coll;
				dist = currentDist;
			}
		}

		return (currentCollider != null) ? currentCollider.transform : null;
	}

	Vector3 CalculateNegativeValues(Vector3 eulerAngles)
	{
		eulerAngles.y = (eulerAngles.y > 180) ? eulerAngles.y - 360 : eulerAngles.y;
		eulerAngles.x = (eulerAngles.x > 180) ? eulerAngles.x - 360 : eulerAngles.x;
		eulerAngles.z = (eulerAngles.z > 180) ? eulerAngles.z - 360 : eulerAngles.z;
		return eulerAngles;
	}

	bool Search()
	{
		if(rayOffset < 0) rayOffset = 0;
		float dist = Vector3.Distance(transform.position + offset, target.position);
		Vector3 lookPos = target.position - stoneHeadRotation.position;
		Debug.DrawRay(stoneHeadRotation.position, center.forward * (turretTrigger.radius + rayOffset));
		Vector3 rotation = Quaternion.Lerp(stoneHeadRotation.rotation, Quaternion.LookRotation(lookPos), smooth * Time.deltaTime).eulerAngles;
		
		rotation.z = 0;
		stoneHeadRotation.eulerAngles = rotation;

		if(dist > turretTrigger.radius + rayOffset)
		{
			target = null;
			return false;
		}
		
		if(IsRaycastHit(center)) return true;
		
		return false;
	}

	bool CheckLayerMask(GameObject obj, LayerMask layers)
	{
		if(((1 << obj.layer) & layers) != 0)
		{
			return true;
		}
	
		return false;
	}

	bool IsRaycastHit(Transform point)
	{
		RaycastHit hit;
		Ray ray = new Ray(point.position, point.forward);
		if(Physics.Raycast(ray, out hit, turretTrigger.radius + rayOffset))
		{
			if(CheckLayerMask(hit.transform.gameObject, layerMask))
			{
				return true;
			}
		}

		return false;
	}

	private void Shot()
	{
		if(!Search()) return;
		curFireRate += Time.deltaTime;
		
		if(curFireRate > fireRate)
		{
			Transform point = bulletPoint;
			curFireRate = 0;

			if(bulletPrefab != null)
			{
				TowerBullet bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity) as TowerBullet;
				bullet.SetBullet(layerMask, point.forward);
			}
		}
	}

	private void Choice()
	{
		curFireRate = fireRate;
		target = FindTarget();
		stoneHeadRotation.rotation = Quaternion.Lerp(stoneHeadRotation.rotation, defaultRot, smooth * Time.deltaTime);

		if(Quaternion.Angle(stoneHeadRotation.rotation, defaultRot) == 0)
		{
			stoneHeadRotation.rotation = defaultRot;
			turretTrigger.enabled = true;
			enabled = false;
		}
	}

	private void LateUpdate()
	{
		if (target != null)
		{
			Shot();
		}
		
		else
		{
			Choice();
		}
	}
}