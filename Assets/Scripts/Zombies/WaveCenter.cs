using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveCenter : MonoBehaviour
{
    
    /*
     * 1 - full cube (decide how wide) (r8,c8)
     * 2 - line (decide how many after eachother) (r1,c8)
     * 3 - random circle
     * 4 - Rectangle (dynamic to difficulty)
     * 
     * 1 and 3 can instantiate special guys (with regular speed??)
     * 
     * gör så att wavecenter rör sig, och dör när alla invaders dör (ELLER försvinner efter en liten stund)
     * gör så att wavecenter flyttar wave inte individuella zombies
     */
    
    public int waveNumber;

    private Vector3 initialPosition;
    public int row;
    public int col;
    public int difficulty; //0->3
    /* 0 - easy, 1-5
     * 1 - medium, 6-10
     * 2 - hard, 11-15
     * 3 - supah zombie mode - 16-20
     */
    public Zombies critterPrefab;
    BoxCollider2D m_Collider;
    
    // Start is called before the first frame update
    void Awake()
    {
        initialPosition = transform.position;
        CreateInvaderGrid();
        m_Collider = GetComponent<BoxCollider2D>();
        m_Collider.size = new Vector2(row, col);
    }

    void Update()
    {
        int zombCount = GetZombieCount();
        if (zombCount == 0)
        {
            Destroy(gameObject);
        }
    }
    void CreateInvaderGrid()
    {
        for (int r = 0; r < row; r++)
        {
            float width = 2f * (col - 1);
            float height = 2f * (row - 1);

            //för att centerar invaders
            Vector2 centerOffset = new Vector2(-width * 0.25f, -height * 0.25f);
            Vector3 rowPosition = new Vector3(centerOffset.x, (r) + centerOffset.y, 0f);

            for (int c = 0; c < col; c++)
            {
                Zombies tempZombie = Instantiate(critterPrefab, transform);

                Vector3 position = rowPosition;
                position.x += c;
                tempZombie.transform.localPosition = position;
            }
        }
    }
    public int GetZombieCount()
    {
        int nr = 0;

        foreach (Transform Zombie in transform)
        {
            if (Zombie.gameObject.activeSelf)
                nr++;
        }
        return nr;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            return;
        }
        else
        {
            Destroy(m_Collider);
            print("I tried to kill myself");
        }
    }
}
