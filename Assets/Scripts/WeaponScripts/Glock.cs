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
        spritePos = new Vector2(0.655f, -12.248f);


    }
}