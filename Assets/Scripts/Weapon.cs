using UnityEngine;
using UnityEngine.Rendering;


public abstract class Weapon : MonoBehaviour
{
    //Variabler
    public int baseAmmo { get; protected set; }

    public int ammo;

    public float fireRate { get; protected set; }
    public float damage { get; protected set; }

    public float projectileSpeed { get; protected set; }

    [SerializeField] protected GameObject bulletTransform;

    public AudioClip soundEffect;


    public abstract void SpawnProjectile();
    public void removeObject()
    {
        Destroy(gameObject);
    }
    public void Start()
    {
        ammo = baseAmmo;
    }

    //fixa sprites för alla vapen 
}
