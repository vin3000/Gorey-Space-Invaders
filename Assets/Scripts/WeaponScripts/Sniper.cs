using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public Sniper()
    {
        baseAmmo = 3;
        ammo = baseAmmo;
        fireRate = 3f;
        damage = 50;
        projectileSpeed = 30f;
    }
    public override void SpawnProjectile()
    {
        Bullet bullet = Instantiate((Bullet) projectilePrefab, bulletTransform.transform.position, transform.rotation);
        bullet.damage = damage;
        bullet.speed = projectileSpeed;
    }
}
