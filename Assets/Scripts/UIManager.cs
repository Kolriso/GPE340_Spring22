using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] hearts;
    public PlayerController player;
    private ResetButton restartGame;
    public Image healthBarImage;
    public bool isPaused = false;
    public GameObject pauseMenu;


    // Start is called before the first frame update
    void Start()
    {
        restartGame = FindObjectOfType<ResetButton>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (isPaused == false)
            {
                Time.timeScale = 0;
                isPaused = true;
                pauseMenu.SetActive(true);
            }
            else
            {
                pauseMenu.SetActive(false);
                isPaused = false;
                Time.timeScale = 1;
            }
        }
    }

    public void ShowGameOverScreen()
    {
        restartGame.ShowCanvas();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarImage.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1.0f);
    }

    public void LooseHeart()
    {
        if (player.lives < 1)
        {
            Destroy(hearts[0].gameObject);
        }
        else if (player.lives < 2)
        {
            Destroy(hearts[1].gameObject);
        }
        else if (player.lives < 3)
        {
            Destroy(hearts[2].gameObject);
        }
    }

    public void UnPauseGame()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
            Debug.Log("Game is Quitting");
        #endif
    }
}
