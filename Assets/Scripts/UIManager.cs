using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image[] hearts;
    private ResetButton restartGame;
    public Image healthBarImage;


    // Start is called before the first frame update
    void Start()
    {
        restartGame = FindObjectOfType<ResetButton>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowGameOverScreen()
    {
        restartGame.ShowCanvas();
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarImage.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1.0f);
    }
}
