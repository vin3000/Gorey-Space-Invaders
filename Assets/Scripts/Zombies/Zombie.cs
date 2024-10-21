using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Zombies : MonoBehaviour
{
    /*
     * G�r s� att game manager letar efter denna ist�llet f�r invaders
     */

    public float speed = 10f;
    private Vector3 direction = Vector3.down;
    public float damage = 10f;
    public float health = 10f;

    public bool isShooter = false;
    public bool isExplosive = false;
    public bool isSummoner = false;
    public bool isInfested = false;
    //public bool isProtector = false;

    public Missile missilePrefab;
    public GameObject summonPrefab;
    public void Start()
    {
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
        if (rand < 0.5)
        {
            Missile shotMissile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
            shotMissile.GetComponent<Missile>().damage = damage;
        }
    }
    void SummoningZombies()
    {
        float rand = UnityEngine.Random.value;
        if (rand < 0.2)
        {
            //kolla f�r ett snyggare s�tt att g�ra detta
            GameObject summon = Instantiate(summonPrefab, new Vector3(transform.position.x-1,transform.position.y,0), Quaternion.identity);
            GameObject summon2 = Instantiate(summonPrefab, new Vector3(transform.position.x + 1, transform.position.y, 0), Quaternion.identity);
            summon.GetComponent<Zombies>().speed = speed + 2f;
            summon2.GetComponent<Zombies>().speed = speed + 2f;
            if (isInfested)
            {
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        Move();
        if (health <= 0 || health == 0)
        {
            Die();
        }
    }

    void Move()
    {
        transform.position += speed * Time.deltaTime * direction;
    }
    void Die()
    {
        //kalla partiklar
        if (isExplosive)
        {
            //kalla explosion
            //temporary
            Destroy(gameObject);
        }
        if (isInfested)
        {
            SummoningZombies(); //g�r s� summon skript kollar hur m�nga som ska summonas
        }
        else
        {

            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser")) //layer name change to bullet?
        {
            health -= collision.gameObject.GetComponent<Bullet>().damage;
        }
        else if ((collision.gameObject.layer == LayerMask.NameToLayer("Boundary"))||(collision.gameObject.layer==LayerMask.NameToLayer("player"))) //n�tt nedre kanten
        {
            GameObject Player = GameObject.Find("Player");
            Player.GetComponent<Health>().currentHealth -= damage; //make it so that it kills a bit of health
            GameManager.Instance.OnBoundaryReached(); //h�r letar game manager efter invaders, n�r koden h�r har blivit individ baserad. MAY OR MAY NOT BE USELESS. I think this is the "damage player if edge" thing

        }
    }
}


//skapa subklasser av zombier. Varje klass ska inneh�lla zombiens actions. Som Invader skripten.
/* critter
 * L�ngsam skjutare
 * Explosiv
 * Boss
 * 
 * summoner
 * blocker
 * infested
 * fast as shit
 */

//�ndra till annan prefab

