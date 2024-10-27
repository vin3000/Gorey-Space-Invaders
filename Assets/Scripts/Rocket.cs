using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Rocket : Projectile
{
    public float damage;
    public float explosionDamage;
    [SerializeField] private Explosion explosionParticle;
    private ParticleSystem explosionPartSys;
    private CameraShake shaker;

    private void Awake()
    {
        shaker = Camera.main.GetComponentInParent<CameraShake>();
        direction = Vector3.up;
        explosionPartSys = explosionParticle.GetComponent<ParticleSystem>();
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
        if (gameObject.transform.position.y > 30)
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
            Explosion explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            explosion.explosionDamage = explosionDamage;
            explosion.soundEffect.PlayOneShot(explosion.soundEffect.clip, 0.05f);
            Destroy(explosion, explosionPartSys.main.duration);
            shaker.StartCoroutine(shaker.Shake(0.5f, 2f));
        }
    }
}
