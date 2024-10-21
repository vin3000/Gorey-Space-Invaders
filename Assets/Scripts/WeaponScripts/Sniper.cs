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
}
