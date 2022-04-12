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
    }

    public void SpawnPlayer()
    {
        Instantiate(playerPrefab, playerSpawnPoint.position, Quaternion.identity);
    }

    // Start game starts when gameplay starts
    public void StartGame()
    {
        
    }

    public void EndGame()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
