using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Zombies : MonoBehaviour
{
    /*
     * Gör så att game manager letar efter denna istället för invaders
     */
    public float speed = 10f;
    public float damage = 10f;
    public float health = 10f;

    protected bool isShooter = false;
    protected bool isExplosive = false;

    public Missile missilePrefab;
    public void Start()
    {
        if (isShooter)
        {
            InvokeRepeating(nameof(MissileAttack), 1, 1);
        }
    }
    void MissileAttack()
    {
        float rand = UnityEngine.Random.value;
        if (rand < 0.2)
        {
            Instantiate(missilePrefab, transform.position, Quaternion.identity);
        }
    }

    private void update()
    {
        if (isExplosive && (health <= 0 || health == 0))
        {
            //kalla explosions-metod
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser")) //layer name change to bullet?
        {
            //GameManager.Instance.OnInvaderKilled(this); 
            //minska health med damage av laser

            //health -= collision.gameObject.GetComponent<Laser eller bullet>().(damage variabeln)
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Boundary")) //nått nedre kanten
        {
            GameObject Player = GameObject.Find("Player");
            //Player.GetComponent<Health>() //make it so that it kills a bit of health
            GameManager.Instance.OnBoundaryReached(); //här letar game manager efter invaders, när koden här har blivit individ baserad.
        }
    }
}


//skapa subklasser av zombier. Varje klass ska innehålla zombiens actions. Som Invader skripten.
/* Liten snabb
 * Långsam skjutare
 * Explosiv
 * Boss
 */

//Ändra till annan prefab

