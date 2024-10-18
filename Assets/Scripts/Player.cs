using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    // Variabler
    public Bullet bulletPrefab;
    private Bullet bullet;
    float speed = 10f;

    //Vapen Variabler
    public Weapon glockPrefab, sniperPrefab;
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
        currentWeapon = Instantiate(glockPrefab, glockPrefab.spritePos, Quaternion.identity, transform);
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
            SwapWeapon(sniperPrefab);
        }
    }
    private void Shoot(int ammo, float fireRate, float damage, float projectileSpeed)
    {
        if (canShoot = true && !waiting)//Kollar om coroutinen Cooldown kör med hjälp av waiting variabeln, så att vi inte startar flera cooldowns.
        {
            if(currentWeapon.ammo <= 0)
            {
                Console.WriteLine("no  more bullets :(");
                currentWeapon = Instantiate(glockPrefab, glockPrefab.spritePos, Quaternion.identity, transform);
                return;
            }
            AudioSource soundEffect = currentWeapon.GetComponent<AudioSource>();
            if(soundEffect != null) {
                soundEffect.Play();
            }
            currentWeapon.SpawnBullet(bulletPrefab);
            StartCoroutine(Cooldown(fireRate));
            currentWeapon.ammo -= 1;
            
        }
    }


    private void SwapWeapon(Weapon newWeapon)
    {
        currentWeapon.removeObject();
        print(currentWeapon);
        currentWeapon = Instantiate(newWeapon, newWeapon.spritePos, newWeapon.transform.rotation, transform);
    }

    IEnumerator Cooldown(float fireRate)
    {
        waiting = true;
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
        waiting = false;
        
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
