using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : Projectile
{
    SpriteRenderer _sprite;
    float interval = 0.2f;
    bool _running = false;
    private void Awake()
    {
        direction = Vector3.down;
    }
    private void FixedUpdate()
    {
        if(_running == false)
        {
            StartCoroutine(swapColor());
        }
        if(transform.position.y < -20)
        {
            Destroy(gameObject);
        }
        transform.position += speed * Time.deltaTime * direction;
    }
    //Lägg till circle collider som anropar spelarens swapweapon metod med ett random nummer
    IEnumerator swapColor()
    {
        _running = true;
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        _sprite.color = Random.ColorHSV();
        yield return new WaitForSeconds(interval);
        _running = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
