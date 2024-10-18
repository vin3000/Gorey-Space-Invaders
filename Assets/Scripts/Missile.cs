using System.Collections;
using System.Collections.Generic;
using System.Data;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;



[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Missile : Projectile
{
//<<<<<<< HEAD

    public int currentHealth;
    public int Damage; 

//=======
    public float damage;
//>>>>>> 1f2bd770fd678bcfb6d8f9347bf2180d81a8d208
    private void Awake()
    {
        direction = Vector3.down;
    }
   
    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject); //så fort den krockar med något så ska den försvinna. 

        

    }

    }





