using UnityEngine;
using UnityEngine.Rendering;


public class Weapon : MonoBehaviour
{
    //Variabler
    public int baseAmmo { get; protected set; }

    public int ammo;

    public float fireRate { get; protected set; }
    public float damage { get; protected set; }

    public float projectileSpeed { get; protected set; }

    public Vector3 spritePos { get; protected set; }

    public void SpawnBullet(Bullet bullet)
    {
        Bullet spawnedBullet = Instantiate(bullet, bullet.transform.Find("BulletTransform").transform.position, Quaternion.identity);
        spawnedBullet.damage = damage;
        spawnedBullet.speed = projectileSpeed;
    }
    public void removeObject()
    {
        Destroy(gameObject);
    }

    //fixa sprites för alla vapen 
}
