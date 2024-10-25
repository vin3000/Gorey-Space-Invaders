using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionDamage;
    public AudioSource soundEffect;

    //L�gg till explosion sound effect, kolla i andra scripts f�r inspiration

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            Destroy(gameObject.GetComponent<CircleCollider2D>());
        }
    }
}
