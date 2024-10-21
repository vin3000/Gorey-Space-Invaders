using UnityEngine;


public class Glock : Weapon
{
    public Glock()
    {
        baseAmmo = int.MaxValue;
        ammo = baseAmmo;
        fireRate = 0.2f;
        damage = 5;
        projectileSpeed = 10f;
    }

    public override void SpawnProjectile()
    {
        Bullet bullet = (Bullet)Instantiate(projectilePrefab, bulletTransform.transform.position, transform.rotation);
        bullet.damage = damage;
        bullet.speed = projectileSpeed;
    }
}