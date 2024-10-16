using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    // Variabler
    public Bullet bulletPrefab;
    public Bullet bullet;
    float speed = 10f;
    public AudioSource source;

    //Vapen Variabler
    public GameObject[] weaponSprites;
    Weapon currentWeapon;
    GameObject currentSprite;
    bool canShoot = true;
    bool waiting = false;



    /* What each weapon needs (skapa prefabs som ärver)
     * Damage
     * Rate of fire
     * Max ammo
    */

    private void Start()
    {
        source = GetComponent<AudioSource>();
        currentWeapon = new Glock();
        currentSprite = Instantiate(weaponSprites[0], currentWeapon.spritePos, Quaternion.identity);
        currentSprite.transform.SetParent(transform, true);
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
        

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot(currentWeapon.ammo, currentWeapon.fireRate, currentWeapon.damage, currentWeapon.projectileSpeed);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwapWeapon(new Sniper(), weaponSprites[1]);
        }
    }
    private void Shoot(int ammo, float fireRate, float damage, float projectileSpeed)
    {
        if (canShoot = true && !waiting)//Kollar om coroutinen Cooldown kör med hjälp av waiting variabeln, så att vi inte startar flera cooldowns.
        {
            if(currentWeapon.ammo <= 0)
            {
                Console.WriteLine("no  more bullets :(");
                currentWeapon = new Glock();
                return;
            }
            source.Play();
            StartCoroutine(Cooldown(fireRate));
            bullet = Instantiate(bulletPrefab, currentSprite.transform.Find("BulletTransform").transform.position, Quaternion.identity);
            bullet.damage = currentWeapon.damage;
            bullet.speed = projectileSpeed;
            currentWeapon.ammo -= 1;
            
        }
    }

    private void SwapWeapon(Weapon newWeapon, GameObject sprite)
    {
        Destroy(currentSprite);
        currentWeapon = newWeapon;
        currentSprite = sprite;
        currentSprite = Instantiate(sprite, newWeapon.spritePos, new Quaternion(0, 0, -90, 0));
        currentSprite.transform.SetParent(transform, true);
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }
}
