using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private Player player;
    private Invaders invaders; //summoner, inte enemy
    private MysteryShip mysteryShip; //soldaten
    private Bunker[] bunkers;
    public Powerup powerupPrefab;

    //Anv�nds ej just nu, men ni kan anv�nda de senare
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>(); //summoner, inte enemy
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return)) //scrap, health mechanic replaces
        {
            NewGame();
        }
    }

    private void NewGame()
    {

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        //invaders.ResetInvaders(); 
        //invaders.gameObject.SetActive(true);

        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }

        Respawn();
    }

    private void Respawn() //reset waves
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void GameOver() //hade varit coolt med en zoom in som i Oscars spel :F
    {
        invaders.gameObject.SetActive(false);
    }

    private void SetScore(int score)
    {
        
    }

    private void SetLives(int lives)
    {
       
    }

    public void OnPlayerKilled(Player player)
    {

        player.gameObject.SetActive(false);

    }

    public void OnInvaderKilled(Invader invader)  //I think this all is useless?? Due to game mechanic changes
    {
        invader.gameObject.SetActive(false);

       

        if (invaders.GetInvaderCount() == 0)
        {
            NewRound();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip, Vector2 deathPos) 
    {
        mysteryShip.gameObject.SetActive(false); //for a time. maybe kill and summon new one, even though player doesnt kill it.
        SpawnPowerup(mysteryShip.transform.position);
    }
    public void SpawnPowerup(Vector2 position)
    {
        Instantiate(powerupPrefab, position, Quaternion.identity);
    }
    public void OnBoundaryReached() //change so that player looses health or something
    {
        /*
       if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);
            OnPlayerKilled(player);
        }
       */ //Kanske ta bort???
    }

}
