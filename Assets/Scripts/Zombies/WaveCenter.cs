using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveCenter : MonoBehaviour
{
    /*
     * 1 - full cube (decide how wide)
     * 2 - line (decide how many after eachother)
     * 3 - random circle
     * 4 - Rectangle (dynamic to difficulty)
     * 
     * 1 and 3 can instantiate special guys (with regular speed??)
     * 
     * 
     */
    public int waveNumber;

    public int row = 8;
    public int col = 8;
    public Zombies critterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        CreateInvaderGrid();
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
            Vector2 centerOffset = new Vector2(-width * 0.5f, -height * 0.5f);
            Vector3 rowPosition = new Vector3(centerOffset.x, (2f * r) + centerOffset.y, 0f);

            for (int c = 0; c < col; c++)
            {
                Zombies tempInvader = Instantiate(critterPrefab, transform);

                Vector3 position = rowPosition;
                position.x += 2f * c;
                tempInvader.transform.localPosition = position;
            }
        }
    }
}
