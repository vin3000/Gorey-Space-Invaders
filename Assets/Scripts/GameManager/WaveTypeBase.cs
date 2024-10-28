using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class WaveTypeBase : MonoBehaviour
{

    public int maxWaves; //start amount of waves

    public int frequency; //every X number of waves, do this (instantiate, increase)
    public int offset; //if it instantiates every 3 waves (8, 11, 14 osv) set to 1 (8+1%3=0)
    public bool isCalledAtBoss; 
    private int timesCalled = 0; //if increases after x times called, max increases every 

    public GameObject wave;
    public Vic_GameManager gameManager;

    //klicka i den som den är
    public bool bigSmall; //används av easyMulti
        //increases and calls big wave if currentround+offset%frequency == 0. else, calls small wave.
    
    public bool countTimesMade;
    public int countFrequency; //how often it increases relative to count, not rounds

    //public bool shooterSquad;
        //öka med 1 var tredje KALLELSE
            //frequency == 2
            //called at boss = true
            //kallas varje x%2 & varje x%5
    //public bool hardMulti;
        //öka med 1 var tredje KALLELSE
            //kallas varje x%2 & varje x%5

    //public bool hardThick;
        //öka med 1 var tredje KALLELSE
            //kallas varje x+1%2 & varje x%5


    // Start is called before the first frame update

    void Start()
    {
    }

    public void IncreaseMax(int currentRound)
    {
        if (bigSmall)
        {
            if((currentRound+offset)%frequency == 0)
            {
                maxWaves += 1;
            }
        }
        if (countTimesMade)
        {
            if(timesCalled == countFrequency)
            {
                Debug.Log("tried increasing through counting");
                maxWaves+=1;
                timesCalled = 0;
            }
        }
    }
    public void AddToInstantiateList(int currentRound, int difficulty)
    {
        if (bigSmall)
        {
            if ((currentRound + offset) % frequency == 0)
            {
                for (int a = 0; a < maxWaves; a++)
                {
                    gameManager.waves.Add(wave);
                }
            }
            else
            {
                for (int a = 0; a < difficulty; a++)
                {
                    gameManager.waves.Add(wave);

                }
            }
        }
        else
        {
            if ((currentRound + offset )% frequency == 0)
            {
                for (int a = 0; a < maxWaves; a++)
                {
                    gameManager.waves.Add(wave);
                }
                if (countTimesMade)
                {
                    timesCalled++;
                    Debug.Log($"regular timesCalled just increased to:{timesCalled}");
                }
            }
            else if (isCalledAtBoss)
            {
                if (currentRound % 5 == 0)
                {
                    for (int a = 0; a < maxWaves; a++)
                    {
                        gameManager.waves.Add(wave);
                    }
                    if (countTimesMade)
                    {
                        timesCalled++;
                        Debug.Log($"boss timesCalled just increased to:{timesCalled}");

                    }
                }
                
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
