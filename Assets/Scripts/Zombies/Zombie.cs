using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Zombies : MonoBehaviour
{
    /*
     * Gör så att game manager letar efter denna istället för invaders
     */

    /*
     * byt ut mot annan projectile?? Bullet?
     */

    public float speed = 10f;
    private Vector3 direction = Vector3.down;
    public float damage = 10f;
    public float health = 10f;

    public bool isShooter = false;
    public bool isExplosive = false;
    public bool isSummoner = false;
    public bool isInfested = false;
    private CameraShake shaker;
    private Freezeframe freezer;
    //public bool isProtector = false;

    public Missile missilePrefab;
    public GameObject summonPrefab;
    public Explosion explosionParticle;
    private ParticleSystem explosionPartSys;
    public GameObject bloodParticle;
    private ParticleSystem bloodPartSys;
    Vector2 scaleOfObject; //used to change size of blood particle


    Animator animator;
    bool isDead; //turns true when it dies, to trigger animation
    float deadAnimTime = 3f;
    //public walkAnimSpeed;
    public void Start()
    {
        Vector2 scaleOfObject = transform.lossyScale;
        animator = GetComponentInChildren<Animator>();
        bloodPartSys = bloodParticle.GetComponent<ParticleSystem>();
        explosionPartSys = explosionParticle.GetComponent<ParticleSystem>();
        shaker = Camera.main.GetComponentInParent<CameraShake>();
        freezer = Camera.main.GetComponentInParent<Freezeframe>();

        if (isShooter)
        {
            InvokeRepeating(nameof(MissileAttack), 1, 1);
        }
        if (isSummoner)
        {
            InvokeRepeating(nameof(SummoningZombies),1,1);
        }
    }
    void MissileAttack()
    {
        float rand = UnityEngine.Random.value;
        if (rand < 0.5 && !isDead)
        {
            Missile shotMissile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            shotMissile.GetComponent<Missile>().damage = damage;
        }
    }
    void SummoningZombies()
    {

        float rand = UnityEngine.Random.value;
        if (isInfested)
        {
            GameObject summon = Instantiate(summonPrefab, new Vector3(transform.position.x - 1, transform.position.y, 0), Quaternion.identity);
            GameObject summon2 = Instantiate(summonPrefab, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
            summon.GetComponent<Zombies>().speed = speed + 2f;
            summon2.GetComponent<Zombies>().speed = speed + 2f;
        }
        else if (rand < 0.3 && !isDead)
        {
            //kolla fï¿½r ett snyggare sï¿½tt att gï¿½ra detta
            GameObject summon = Instantiate(summonPrefab, new Vector3(transform.position.x-1,transform.position.y,0), Quaternion.identity);
            GameObject summon2 = Instantiate(summonPrefab, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
            summon.GetComponent<Zombies>().speed = speed + 2f;
            summon2.GetComponent<Zombies>().speed = speed + 2f;
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            Move();
        }
    }

    void Move()
    {
        transform.position += speed * Time.deltaTime * direction;
    }
    void CheckHealth()
    {
        if (health <= 0 || health == 0)
        {
            Die();
        }
    }
    void Die()
    {
        if(transform.parent != null)
        {
            transform.parent = null;
        }
        freezer.freeze(0.2f);
        BoxCollider2D bCol = GetComponent<BoxCollider2D>();
        bCol.enabled = !bCol.enabled;

        //kalla partiklar
        if (isExplosive)
        {
            Explosion explosion = Instantiate(explosionParticle, transform.position, Quaternion.identity);
            explosion.explosionDamage = damage;
            explosion.soundEffect.PlayOneShot(explosion.soundEffect.clip, 0.05f);
            Destroy(explosion, explosionPartSys.main.duration);
        }
        if (isInfested)
        {
            SummoningZombies(); //gï¿½r sï¿½ summon skript kollar hur mï¿½nga som ska summonas
        }
        isDead = true;
        animator.SetBool("isDead", isDead);
        Destroy(gameObject, deadAnimTime);
        
    }

  /*  IEnumerator shakeNFreeze()
    {
        freezer.freeze(0.1f);
        yield return new WaitForSeconds(0.1f);
        shaker.StartCoroutine(shaker.Shake(0.2f, 3f));
    }
  */ //kanske lägg tillbkas
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser")) //layer name change to bullet?
        {
            if (collision.gameObject.GetComponent<Bullet>())
            {
                health -= collision.gameObject.GetComponent<Bullet>().damage;
                CheckHealth();
                Object blood = Instantiate(bloodParticle, transform.position, Quaternion.identity);
                Destroy(blood, 1f);
            }
            if (collision.gameObject.GetComponent<Rocket>()) 
            {
                health -= collision.gameObject.GetComponent<Rocket>().damage;
                CheckHealth();
            }
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            health -= collision.gameObject.GetComponent<Explosion>().explosionDamage;
            CheckHealth();
            // lägg till en explosion damage variabel på explosion grejen
        }
        else if ((collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))||(collision.gameObject.layer==LayerMask.NameToLayer("Player"))) //nï¿½tt nedre kanten
        {
            print(collision.gameObject);
            GameObject Player = GameObject.Find("Player");
            Player.GetComponent<Health>().currentHealth -= damage; //make it so that it kills a bit of health
            GameManager.Instance.OnBoundaryReached(); //hï¿½r letar game manager efter invaders, nï¿½r koden hï¿½r har blivit individ baserad. MAY OR MAY NOT BE USELESS. I think this is the "damage player if edge" thing
        }
    }
}



//skapa subklasser av zombier. Varje klass ska innehï¿½lla zombiens actions. Som Invader skripten.
/* critter
 * Lï¿½ngsam skjutare
 * Explosiv
 * Boss
 * 
 * summoner
 * blocker
 * infested
 * fast as shit
 */

//ï¿½ndra till annan prefab

