using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Vic_GameManager : MonoBehaviour
{
    int difficulty; //increase difficulty method linked to rounds
    int currentRound = 0;     //increase rounds metod
    public int finalRound = 20;
    List<GameObject> waves = new List<GameObject>();
    /*
    public GameObject[] easyWaves = new GameObject[1];
    public GameObject[] mediumWaves = new GameObject[1]; //dessa tre fyller ut
    public GameObject[] hardWaves = new GameObject[1];
    */
    public Zombies[] specZombies = new Zombies[4]; //zombies som spawnar randomly (0-Expl, 1-*//* + expl, 2- *//* + infest, 3- *//* + one extra explode)
    public GameObject[] spawnPoints = new GameObject[16]; //alla platser som zombies kan skapas. Ifall nuddar kant, flyttar 

    int easyWaveSize = 3;
    int mediumWaveSize = 0;
    int hardWaveSize = 0;
    int bossAmount = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
         * kolla ifall det finns n�gra zombier kvar i scenen, och ifall rundans lista �r tom
         * ifall det �r det, advance round
         */
    }
    void AdvanceRound()
    {
        currentRound += 1;
        if(currentRound-1%5 == 0 && currentRound != 1) //is called every 5th wave from 1, so 6, 11 and so on. (doesnt call on 1, because difficulty starts at 0)
        {
            difficulty += 1;
        }
        IncreaseMax();
        /*
        *varje round;
        * �ka current round
        * �ka difficulty (ifall det ska)
        * uppdatera max av xWaveSize
        * kalla metoder f�r att fylla listor (och fyll ut resten med regular prefabs)
        *       dessa metoder ska finnas i prefabs f�r sina respektive wave types. Ska h�lla koll p� hur m�nga som beh�vs.
        * 1) l�gg allting direkt i listan, och h�ll koll p� hur m�nga g�nger det har gjorts med n�n int variabel
        * 2) l�gg allting i egna listor, sedan merga dem
        * blanda listan
        * 1) shuffla listan
        * 2) skapa random fr�n listan mellan 1-listcount
        * instantiera, med cooldown (h�mta "instantiateCooldown" fr�n Zombies eller wave center
        * */

    }
    void IncreaseMax()
    {
        //easy increase
        if (currentRound + 1 % 2 == 0 && difficulty==0)
        {
            easyWaveSize += 1;
        }else if(difficulty != 0 && currentRound - 1 % 5 == 0)
        {
            easyWaveSize += 1;
        }

        //medium increase
        if (currentRound + 1 % 2 == 0 && difficulty == 1)
        {
            mediumWaveSize += 1;
        }else if (difficulty > 1 && currentRound-1 % 3 == 0)
        {
            mediumWaveSize += 1;
        } //ifall difficulty �r 0 s� h�nder inget, medium �r 0

        //hard increase
        if (currentRound+1%3 == 0 && difficulty >= 2)
        {
            hardWaveSize += 1;
        }
        if (currentRound == finalRound)
        {
            hardWaveSize += 3;
        }
    }
}
