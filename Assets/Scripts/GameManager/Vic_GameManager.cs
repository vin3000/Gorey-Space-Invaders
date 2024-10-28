using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class Vic_GameManager : MonoBehaviour
{
    //make game manager respawn mystery ships

    int difficulty = 0; //increase difficulty method linked to rounds
    [SerializeField]
    int currentRound = 0;     //increase rounds metod //TEMPORARILLY PUBLIC
    public int finalRound = 20;
    public List<GameObject> waves = new List<GameObject>();
    int waves_Length;

    public WaveTypeBase easyMulti;
    public WaveTypeBase shooterSquad;
    public WaveTypeBase hardShooter;
    public WaveTypeBase hardThick;

    public Zombies[] specZombies = new Zombies[4]; //zombies som spawnar randomly (0-Expl, 1-*//* + expl, 2- *//* + infest, 3- *//* + one extra explode)
    public GameObject spawnPointObject;
    Vector2 spawnPoint; //points mellan -16 och 16. 
    int minSpawn = -16;
    int maxSpawn = 16;
    private bool waveCreationComplete;

    int numOfNormWaves = 2;
    public GameObject[] easyWaves = new GameObject[2];
    public GameObject[] mediumWaves = new GameObject[2];
    public GameObject[] hardWaves = new GameObject[2];
    public GameObject boss;

    int easyWaveSize = 3;
    int mediumWaveSize = 1;
    int hardWaveSize = 0;
    int bossAmount = 0;

    void Start()
    {
        spawnPoint = spawnPointObject.transform.position;
        AdvanceRound();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N)) //remove when building
        {
            waves.Clear();
            AdvanceRound();
        }
        int waveCount = GetWaveCount();
        if (waveCount == 0 && waveCreationComplete)
        {
            AdvanceRound();
            Debug.Log("NEXT ROUND BABYYY");
        }
    }
    public int GetWaveCount()
    {
        int nr = 0;

        foreach (Transform Waves in transform)
        {
            if (Waves.gameObject.activeSelf)
                nr++;
        }
        return nr;
    }
    void AdvanceRound()
    {
        currentRound += 1;
        if((currentRound-1)%5 == 0 && currentRound != 1) //is called every 5th wave from 1, so 6, 11 and so on. (doesnt call on 1, because difficulty starts at 0)
        {
            //Debug.Log("increased difficulty to " + difficulty);
            difficulty += 1;
        }
        IncreaseMax();
        if (difficulty >= 0)
        {
            easyMulti.IncreaseMax(currentRound);
            easyMulti.AddToInstantiateList(currentRound, difficulty);
            waves_Length = waves.Count;
            //Debug.Log("wavelength = " + waves_Length);
            int remainingSpots = easyWaveSize - waves_Length;
            //Debug.Log($"easy remaining spots = {remainingSpots}");
            for(int a = 0; a < remainingSpots; a++)
            {
                int rand = UnityEngine.Random.Range(0, numOfNormWaves);
                //Debug.Log($"easy rand = {rand}");
                waves.Add(easyWaves[rand]);
            }
        }
        if (difficulty >= 1)
        {
            shooterSquad.IncreaseMax(currentRound);
            shooterSquad.AddToInstantiateList(currentRound, difficulty);
            waves_Length = waves.Count;
            Debug.Log($"easywavesize = {easyWaveSize}. waveslength = {waves_Length}. Mediumwavesize = {mediumWaveSize}");
            int remainingSpots = easyWaveSize + mediumWaveSize - waves_Length;   //waves length har nu 7
            Debug.Log($"medium remaining spots = {remainingSpots}");
            for (int a = 0; a < remainingSpots; a++)
            {
                int rand = UnityEngine.Random.Range(0, numOfNormWaves);
                Debug.Log($"medium rand = {rand}");
                waves.Add(mediumWaves[rand]);
            }
        }
        if (difficulty >= 2)
        {
            hardShooter.IncreaseMax(currentRound);
            hardThick.IncreaseMax(currentRound);
            hardShooter.AddToInstantiateList(currentRound, difficulty);
            hardThick.AddToInstantiateList(currentRound, difficulty);
            waves_Length = waves.Count;
            int remainingSpots = easyWaveSize + mediumWaveSize + hardWaveSize - waves_Length;
            Debug.Log($"hard remaining spots = {remainingSpots}");
            for (int a = 0; a < remainingSpots; a++)
            {
                int rand = UnityEngine.Random.Range(0, numOfNormWaves);
                Debug.Log($"hard rand = {rand}");
                waves.Add(hardWaves[rand]);
            }
        }
        if (bossAmount != 0)
        {
            for(int a = 0; a < bossAmount; a++)
            {
                waves.Add(boss);
            }
        }
        float waitTime;
        waitTime = PlaceWaves(waves.Count);
        waves_Length = waves.Count;
        StartCoroutine(Cooldown(waitTime));
    }
    IEnumerator Cooldown(float waitTime)
    {
        waveCreationComplete = false;
        for (int t = 0; t < waves_Length; t++)   ///////////////FIXA KOD HÄR
        {
            yield return new WaitForSeconds(waitTime);
            int remainingWaves = waves.Count;
            waitTime = PlaceWaves(remainingWaves);
            Debug.Log($"{t} waittime = {waitTime}");
            Debug.Log($"{t} coroutine finished. Summoning new wave");
        }
        waveCreationComplete = true;
    }
    float PlaceWaves(int remainingWaves)
    {
        int randFromList = UnityEngine.Random.Range(0, remainingWaves); //slumpat nummer från lista av waves
        Debug.Log($"randfromlist = {randFromList}");
        GameObject tempWave = Instantiate(waves[randFromList], transform); //skapar random wave
        waves.RemoveAt(randFromList); //tar bort från lista

        WaveCenter waveCenter = tempWave.GetComponent<WaveCenter>();
        int width = waveCenter.colSize / 2;
        Vector2 position = new Vector2(UnityEngine.Random.Range(minSpawn+width, maxSpawn+1-width), spawnPoint.y); //placerar objektet någonstans där zombier inte är utanför kanten
        tempWave.transform.localPosition = position;
        if (waveCenter.isMultiWave)
        {
            for (int t = 0; t < waveCenter.multiWaveNumber - 1; t++)
            {
                GameObject tWave = Instantiate(tempWave, transform);
                tWave.transform.localPosition = new Vector2(position.x,position.y+(t+1)*5);
            }
        }

        float waitTime = waveCenter.instantiateCooldown; //hämtar waittime, som säger hur lång tid det ska ta innan nästa wave får komma
        return (waitTime);
    }

    void IncreaseMax()
    {
        
        //Debug.Log($"current round + 1 = {currentRound + 1}. {(currentRound + 1) % 2 == 0} = {(currentRound+1)%2}. {difficulty}");
        //easy increase
        if ((currentRound + 1) % 2 == 0 && difficulty==0)
        {
            easyWaveSize += 1;
        }else if(difficulty != 0 && (currentRound - 1) % 5 == 0)
        {
            easyWaveSize += 1;
        }

        //medium increase
        if (currentRound % 2 == 0 && difficulty == 1)
        {
            mediumWaveSize += 1;
        }else if (difficulty > 1 && (currentRound-1) % 3 == 0)
        {
            mediumWaveSize += 1;
        } //ifall difficulty är 0 så händer inget, medium är 0

        
        //hard increase
        if ((currentRound+1)%3 == 0 && difficulty >= 2)
        {
            hardWaveSize += 1;
        }
        if (currentRound == finalRound)
        {
            hardWaveSize += 2; //2 extra pga final wave
        }

        //boss increase
        if (difficulty != 3)
        {
            bossAmount = 0;
            if (currentRound % 5 == 0)
            {
                bossAmount = difficulty + 1;
            }
        }
        else if(difficulty==3 && currentRound != finalRound)
        {
            bossAmount = 1;
            if ((currentRound + 1) % 2 == 0)
            {
                bossAmount += 1;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Waves"))
        {

        }
    }
}
