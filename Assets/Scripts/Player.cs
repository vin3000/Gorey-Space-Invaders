using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    // Variabler
    float speed = 10f;
    KeyCode shootKey = KeyCode.Space;
    public Slider ammoBar;

    //Vapen Variabler
    public Weapon glockPrefab, sniperPrefab, rpgPrefab, smgPrefab;
    Weapon currentWeapon;
    bool canShoot = true;
    bool waiting = false;



    /* What each weapon needs (skapa prefabs som ärver)
     * Damage
     * Rate of fire
     * Max ammo
    */

    private void Start()
    {
        currentWeapon = Instantiate(glockPrefab, glockPrefab.transform.position, glockPrefab.transform.rotation);
        currentWeapon.transform.SetParent(transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

        transform.position = position;
        

        if (Input.GetKey(shootKey))
        {
            Shoot(currentWeapon.ammo, currentWeapon.fireRate, currentWeapon.damage, currentWeapon.projectileSpeed);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(sniperPrefab);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwapWeapon(rpgPrefab);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwapWeapon(smgPrefab);
        }
    }
    private void Shoot(int ammo, float fireRate, float damage, float projectileSpeed)
    {
        if (canShoot = true && !waiting)//Kollar om coroutinen Cooldown kör med hjälp av waiting variabeln, så att vi inte startar flera cooldowns.
        {
            if(currentWeapon.ammo <= 0)
            {
                Console.WriteLine("no  more bullets :(");
                currentWeapon.removeObject();
                SwapWeapon(glockPrefab);
                UpdateAmmoBar();
            }
            AudioSource soundEffect = currentWeapon.GetComponent<AudioSource>();
            if(soundEffect != null) {
                soundEffect.PlayOneShot(soundEffect.clip, 0.10f);
            }
            //bullet = Instantiate(bulletPrefab, bulletPrefab.GameObject.Find("BulletTransform").transform.position, Quaternion.identity);
            currentWeapon.SpawnProjectile();
            StartCoroutine(Cooldown(fireRate));  
            currentWeapon.ammo -= 1;
            UpdateAmmoBar();

        }
    }

    private void UpdateAmmoBar()
    {
        ammoBar.value = ((float)currentWeapon.ammo / (float)currentWeapon.baseAmmo);
    }

    private void SwapWeapon(Weapon newWeapon)
    {
        currentWeapon.removeObject();
        currentWeapon = Instantiate(newWeapon, newWeapon.transform.position, newWeapon.transform.rotation);
        currentWeapon.transform.SetParent(transform, false);
        UpdateAmmoBar();
    }

    IEnumerator Cooldown(float fireRate)
    {
        waiting = true;
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        waiting = false;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Powerup"))
        {
            List<Weapon> listOfSpecialWeapons = new();
            listOfSpecialWeapons.Add(sniperPrefab);
            listOfSpecialWeapons.Add(rpgPrefab);
            listOfSpecialWeapons.Add(smgPrefab);
            SwapWeapon(listOfSpecialWeapons[UnityEngine.Random.Range(0, listOfSpecialWeapons.Count)]);
            Destroy(collision.gameObject);
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }*/
}
