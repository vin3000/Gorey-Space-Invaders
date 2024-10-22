using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Weapon
{
    public Rocket rocketPrefab;
    private float explosionDamage = 100f;
    public RPG()
    {
        baseAmmo = 5;
        ammo = baseAmmo;
        fireRate = 2.5f; //ändra tillbaka, bara för testing
        damage = 50;
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
