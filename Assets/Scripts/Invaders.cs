using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    /*Fixa animations
     * varf�r d�r invaders vid toppen?
     * �ndra s� att spelaren skadas ifall en zombie springer f�rbi (kanske mot spelaren?)
     * byt ut mot annan projectile??
     * g�r s� att koden �ndrar waves (helst dynamic waves, inte hardcoded)
     */

    public Invader[] prefab = new Invader[5];

    private int row = 5;
    private int col = 11;

    private Vector3 initialPosition;
    private Vector3 direction = Vector3.down;

    //byt ut mot annan projectile??
    public Missile missilePrefab;

    private void Awake()
    {
        initialPosition = transform.position;
        CreateInvaderGrid();
    }

    private void Start()
    {
        //InvokeRepeating(nameof(MissileAttack), 1, 1); //�ndra till att bara kallas av vissa zombies
    }

    //Skapar sj�lva griden med alla invaders.
    void CreateInvaderGrid()
    {
        for (int r = 0; r < row; r++)
        {
            float width = 2f * (col - 1);
            float height = 2f * (row - 1);

            //f�r att centerar invaders
            Vector2 centerOffset = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(centerOffset.x, (2f * r) + centerOffset.y, 0f);

            for (int c = 0; c < col; c++)
            {
                Invader tempInvader = Instantiate(prefab[r], transform);

                Vector3 position = rowPosition;
                position.x += 2f * c;
                tempInvader.transform.localPosition = position;
            }
        }
    }


    //Aktiverar alla invaders igen och placerar fr�n ursprungsposition
    public void ResetInvaders()
    {

        //g�r s� att koden �ndrar waves (helst dynamic waves, inte hardcoded)
        direction = Vector3.down;
        transform.position = initialPosition;

        foreach (Transform invader in transform)
        {
            invader.gameObject.SetActive(true);
        }
    }

    //Skjuter slumpm�ssigt iv�g en missil. Kolla igenom s� att det passar!
    void MissileAttack()
    {
        int nrOfInvaders = GetInvaderCount();

        if (nrOfInvaders == 0)
        {
            return;
        }

        foreach (Transform invader in transform)
        {

            if (!invader.gameObject.activeInHierarchy) //om en invader �r d�d ska den inte kunna skjuta...
                continue;


            float rand = UnityEngine.Random.value;
            if (rand < 0.2)
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }

    }

    //Kollar hur m�nga invaders som lever
    public int GetInvaderCount()
    {
        int nr = 0;

        foreach (Transform invader in transform)
        {
            if (invader.gameObject.activeSelf)
                nr++;
        }
        return nr;
    }

    //Flyttar invaders �t sidan
    void Update()
    {
        //�ndra s� att spelaren skadas ifall en zombie springer f�rbi (kanske mot spelaren?)

        float speed = 1f;
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
        }
    }
    //Byter riktning och flytter ner ett steg.
    void AdvanceRow()
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }
    /*public Invader[] prefab = new Invader[5];

    private int row = 5;
    private int col = 11;

    private Vector3 initialPosition;
    private Vector3 direction = Vector3.right;

    public Missile missilePrefab;

    private void Awake()
    {
        initialPosition = transform.position;
        CreateInvaderGrid();
    }

    private void Start()
    {
        InvokeRepeating(nameof(MissileAttack), 1, 1); //Hur ofta ska den skjuta iv�g missiler
    }

    //Skapar sj�lva griden med alla invaders.
    void CreateInvaderGrid()
    {
        for(int r = 0; r < row; r++)
        {
            float width = 2f * (col - 1);
            float height = 2f * (row - 1);

            //f�r att centerar invaders
            Vector2 centerOffset = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(centerOffset.x, (2f * r) + centerOffset.y, 0f);
            
            for (int c = 0; c < col; c++)
            {
                Invader tempInvader = Instantiate(prefab[r], transform); 

                Vector3 position = rowPosition;
                position.x += 2f * c;
                tempInvader.transform.localPosition = position;
            }
        }
    }
    
    //Aktiverar alla invaders igen och placerar fr�n ursprungsposition
    public void ResetInvaders()
    {
        direction = Vector3.right;
        transform.position = initialPosition;

        foreach(Transform invader in transform)
        {
            invader.gameObject.SetActive(true);
        }
    }

    //Skjuter slumpm�ssigt iv�g en missil.
    void MissileAttack()
    {
        int nrOfInvaders = GetInvaderCount();

        if(nrOfInvaders == 0)
        {
            return;
        }

        foreach(Transform invader in transform)
        {

            if (!invader.gameObject.activeInHierarchy) //om en invader �r d�d ska den inte kunna skjuta...
                continue;
            
           
            float rand = UnityEngine.Random.value;
            if (rand < 0.2)
            {
                Instantiate(missilePrefab, invader.position, Quaternion.identity);
                break;
            }
        }
       
    }

    //Kollar hur m�nga invaders som lever
    public int GetInvaderCount()
    {
        int nr = 0;

        foreach(Transform invader in transform)
        {
            if (invader.gameObject.activeSelf)
                nr++;
        }
        return nr;
    }

    //Flyttar invaders �t sidan
    void Update()
    {
        float speed = 1f;
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
        }
    }
    //Byter riktning och flytter ner ett steg.
    void AdvanceRow()
    {
        direction = new Vector3(-direction.x, 0, 0);
        Vector3 position = transform.position;
        position.y -= 1f;
        transform.position = position;
    }*/
}
