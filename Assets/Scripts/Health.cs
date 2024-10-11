using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int currentHealth;
    public int maxHealth = 10;
    public int damage = 2;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; 
    }


    // Update is called once per frame
    void Update()
    {
        Death(); 

        TakeDamage();
    }
    
    void TakeDamage()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            currentHealth -= damage; 
        }
    }

    void Death()
    {
        if (currentHealth == 0 || currentHealth < 0)
        {
            Destroy(gameObject);
            //Dödar playern om hälsan (health) är 0 eller är mindre än 0
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //make missile ta damage på playern 
        //make missile ta damage på playern 
    }



}
