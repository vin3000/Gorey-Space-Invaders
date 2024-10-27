using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : Weapon
{
    public Bullet sniperBulletPrefab;
    public Sniper()
    {
        baseAmmo = 5;
        fireRate = 3f;
        damage = 50;
        projectileSpeed = 30f;
    }
    public override void SpawnProjectile()
    {
        Bullet bullet = Instantiate(sniperBulletPrefab, bulletTransform.transform.position, transform.rotation);
        bullet.damage = damage;
        bullet.speed = projectileSpeed;
    }
}
