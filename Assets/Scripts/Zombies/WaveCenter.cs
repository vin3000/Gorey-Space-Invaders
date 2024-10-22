using System.Collections;
using System.Collections.Generic;
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
     */
    public int waveNumber;

    public int row;
    public int col;
    public Zombies critterPrefab;
    BoxCollider2D m_Collider;
    
    // Start is called before the first frame update
    void Start()
    {
        CreateInvaderGrid();
        m_Collider = GetComponent<BoxCollider2D>();
        //These are the starting sizes for the Collider component

        m_Collider.size = new Vector2(row, col);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
