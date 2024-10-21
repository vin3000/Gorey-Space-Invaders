using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Weapon
{
    public RPG()
    {
        baseAmmo = 3;
        ammo = baseAmmo;
        fireRate = 3f;
        damage = 50;
        projectileSpeed = 30f;
    }

    public override void SpawnProjectile()
    {
        Rocket rocket = Instantiate((Rocket) projectilePrefab, bulletTransform.transform.position, transform.rotation);
        rocket.damage = damage;
        rocket.speed = projectileSpeed;
    }
}
