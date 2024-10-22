using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPG : Weapon
{
    public Rocket rocketPrefab;
    public GameObject explosionParticle;
    public RPG()
    {
        baseAmmo = 5; //�ndra tillbaka, bara f�r testing
        ammo = baseAmmo;
        fireRate = 0.1f; //�ndra tillbaka, bara f�r testing
        damage = 50;
        projectileSpeed = 30f;
    }

    public override void SpawnProjectile()
    {
        Rocket rocket = Instantiate(rocketPrefab, bulletTransform.transform.position, transform.rotation);
        print("changed");
        print(rocket.transform.position);
        rocket.damage = damage;
        rocket.speed = projectileSpeed;

        CircleCollider2D rocketCollider = explosionParticle.GetComponent<CircleCollider2D>();
    }
}
