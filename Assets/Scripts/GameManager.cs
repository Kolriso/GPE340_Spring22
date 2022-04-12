using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class represents the game! So: Everything that is in our game should be accessable from here.
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Level Settings")]
    public GameObject playerPrefab;
    public Transform playerSpawnPoint;

    [Header("Player Data")]
    public PlayerController player;
    public int startingLives = 3;

    [Header("UI")]
    public UIManager uiManager;
    public bool isGamePlaying = false;

    private void Awake()
    {
        // The first Game manager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // There's already a game manager, so destroy myself
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Show main menu
        // Temp Delete Me--Start the game immediately
        StartGame();
    }

    public void SpawnPlayer()
    {
        GameObject playerObject = Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity) as GameObject;
        player.pawn = playerObject.GetComponent<Pawn>();
        player.lives--;
    }

    // Start game starts when gameplay starts
    public void StartGame()
    {
        isGamePlaying = true;
        player.lives = startingLives;
    }

    public void EndGame()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckForRespawn();
    }

    private void CheckForRespawn()
    {
        if (isGamePlaying)
        {
            if (player.pawn == null)
            {
                if (player.lives > 0)
                {
                    SpawnPlayer();
                }
                else
                {
                    uiManager.ShowGameOverScreen();
                }
            }
        }
    }
}
