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
    

    //Ammo Variabler
    int ammo = 10;
    float fireRate = 0.2f;
    bool canShoot = true;
    


    /* What each weapon needs (skapa prefabs som ärver)
     * Damage
     * Rate of fire
     * Max ammo
    */

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
            if(canShoot = true)
            {
                Shoot();
                StartCoroutine(Cooldown());
            }
        }
    }
    private void Shoot()
    {
        laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);
    }
    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireRate);
        canShoot = true;
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
