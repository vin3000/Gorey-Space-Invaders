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
    //fixa sprites för alla vapen 
    public GameObject weaponSprite;
}
