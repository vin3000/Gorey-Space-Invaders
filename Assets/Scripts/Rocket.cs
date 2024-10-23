using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Rocket : Projectile
{
    public float damage;
    public float explosionDamage;
    public Explosion explosionParticle;
    private ParticleSystem explosionPartSys;

    private void Awake()
    {
        direction = Vector3.up;
        explosionPartSys = explosionParticle.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
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
            Explosion explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            explosion.explosionDamage = explosionDamage;
            Destroy(explosion, explosionPartSys.main.duration);
        }
    }
}
