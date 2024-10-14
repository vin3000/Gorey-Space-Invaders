using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    // Variabler
    public Laser laserPrefab;
    Laser laser;
    float speed = 5f;

    //Vapen Variabler
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
        currentWeapon = new Glock();
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
            Shoot(currentWeapon.ammo, currentWeapon.fireRate, currentWeapon.damage);
        }
    }
    private void Shoot(int ammo, float fireRate, float damage)
    {
        if (canShoot = true && !waiting)//Kollar om coroutinen Cooldown kör med hjälp av waiting variabeln, så att vi inte startar flera cooldowns.
        {
            StartCoroutine(Cooldown(fireRate));
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
        }
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
