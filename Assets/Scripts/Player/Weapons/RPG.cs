using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Weapon
{
    [SerializeField] private Rocket rocketPrefab;
    private float explosionDamage = 50f;
    public RPG()
    {
        baseAmmo = 3;
        fireRate = 2.5f;
        damage = 40;
        projectileSpeed = 20f;
    }


    public override void SpawnProjectile()
    {
        Rocket rocket = Instantiate(rocketPrefab, bulletTransform.transform.position, transform.rotation);
        rocket.damage = damage;
        rocket.explosionDamage = explosionDamage;
        rocket.speed = projectileSpeed;
    }
}
