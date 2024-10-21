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

    public GameObject bulletTransform;

    public void SpawnBullet(Bullet bulletPrefab)
    {
        Bullet bullet = Instantiate(bulletPrefab, bulletTransform.transform.position, transform.rotation);
        bullet.damage = damage;
        bullet.speed = projectileSpeed;
    }
    public void removeObject()
    {
        Destroy(gameObject);
    }

    //fixa sprites för alla vapen 
}
