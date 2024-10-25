using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Weapon
{
    public Rocket rocketPrefab;
    private float explosionDamage = 50f; //�ndra senare
    public RPG()
    {
        baseAmmo = 3;
        fireRate = 2.5f; //�ndra tillbaka, bara f�r testing
        damage = 40; //�ndra senare
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
