using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float currentHealth;
    private float maxHealth = 100f;
    private float damage = 10f;
    public float heal = 2f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile")) 
        {
            //GameManager.Instance.OnInvaderKilled(this); 
            //minska health med damage av laser
            currentHealth -= collision.gameObject.GetComponent<Missile>().damage;
        }
    }


    public HealthBar healthBar; 

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetMaxHealth(maxHealth); 
    }


    // Update is called once per frame
    void Update()
    {
        Death(); 

        TakeDamage();

        Heal(); 


    }
    
    void TakeDamage()
    {

      

        if (Input.GetKeyDown(KeyCode.A))
        {
            currentHealth -= damage; 
        } //G�r att spelaren tar damage n�r man trycker p� A 


        healthBar.SetHealth(currentHealth); 
        //Uppdaterar healthbar UI 
    } 

    void Heal() {

        if (Input.GetKeyDown(KeyCode.S))
        {
            currentHealth += heal;
        }


    }

    void Death()
    {
        if (currentHealth == 0 || currentHealth < 0)
        {
            Destroy(gameObject);
            //D�dar playern om h�lsan (health) �r 0 eller �r mindre �n 0
        }
    }


   

}
