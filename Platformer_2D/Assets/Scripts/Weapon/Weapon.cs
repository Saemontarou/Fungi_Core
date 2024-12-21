using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform axesPoint;
    public GameObject axe;
    
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }

    }

    void Shoot()
    {
        Instantiate(axe, axesPoint.position, axesPoint.rotation);
    }
}
