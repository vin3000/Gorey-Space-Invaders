using UnityEngine;

public class Glock : Weapon
{
    public Bullet bulletPrefab;

    public Glock()
    {
        baseAmmo = int.MaxValue;
        fireRate = 0.4f;
        damage = 5;
        projectileSpeed = 10f;
    }

    public override void SpawnProjectile()
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletTransform.transform.position, transform.rotation);
        bullet.damage = damage;
        bullet.speed = projectileSpeed;
    }
}