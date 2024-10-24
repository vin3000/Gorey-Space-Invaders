using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Bullet : Projectile
{
    public float damage;
    private void Awake()
    {
        direction = Vector3.up;
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
        if(gameObject.transform.position.y > 30)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    void CheckCollision(Collider2D collision)
    {
        Bunker bunker = collision.gameObject.GetComponent<Bunker>();

        if(bunker == null) //Om det inte är en bunker vi träffat så ska skottet försvinna.
        {
            Destroy(gameObject);
        }
    }
}
