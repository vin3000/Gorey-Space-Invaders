using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritterZombie : Zombies
{
    private void Awake()
    {
        speed = 15f;
        health = 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        /*float speed = 1f;
        transform.position += speed * Time.deltaTime * direction;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        foreach (Transform invader in transform)
        {
            if (!invader.gameObject.activeInHierarchy) //Kolla bara invaders som lever
                continue;

            if (direction == Vector3.right && invader.position.x >= rightEdge.x - 1f)
            {
                AdvanceRow();
                break;
            }
            else if (direction == Vector3.left && invader.position.x <= leftEdge.x + 1f)
            {
                AdvanceRow();
                break;
            }
        }*/
    }
}
