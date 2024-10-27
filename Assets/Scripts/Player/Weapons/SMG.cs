using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    [SerializeField] private Bullet bulletPrefab;

    public SMG()
    {
        baseAmmo = 64;
        fireRate = 0.15f;
        damage = 2.5f;
        projectileSpeed = 15f;
    }

    public override void SpawnProjectile()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletTransform.transform.position, transform.rotation);
        bullet.damage = damage;
        bullet.speed = projectileSpeed;
    }
}
