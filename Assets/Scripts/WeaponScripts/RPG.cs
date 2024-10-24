using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Weapon
{
    public Rocket rocketPrefab;
    private float explosionDamage = 50f; //ändra senare
    public RPG()
    {
        baseAmmo = 3;
        fireRate = 2.5f; //ändra tillbaka, bara för testing
        damage = 40; //ändra senare
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
