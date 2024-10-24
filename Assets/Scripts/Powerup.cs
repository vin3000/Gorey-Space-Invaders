using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Projectile
{
    SpriteRenderer _sprite;
    float interval = 0.25f;
    bool _running = false;
    private void Awake()
    {
        direction = Vector3.down;
    }
    private void FixedUpdate()
    {
        if(_running == false)
        {
            StartCoroutine(weaponRoulette());
        }
        if(transform.position.y < -20)
        {
            Destroy(gameObject);
        }
        transform.position += speed * Time.deltaTime * direction;
    }
    IEnumerator weaponRoulette()
    {
        _running = true;
        foreach (Transform weapon in transform)
        { //Sätter vapnena till active och false om och om igen så att det blir som en roulette
            print(weapon.gameObject.name);
            weapon.gameObject.SetActive(true);
            yield return new WaitForSeconds(interval);
            weapon.gameObject.SetActive(false);
        }
        _running = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
