using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShootEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject effect = Instantiate(_effect.gameObject, transform);
        _effect.Play();
    }
}