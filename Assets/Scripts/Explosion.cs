using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float explosionDamage;
    public AudioSource soundEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            StartCoroutine(screenshake.Shake(0.5f, 0.20f));
        }
    }
}
