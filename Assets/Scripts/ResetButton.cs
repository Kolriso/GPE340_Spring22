using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public GameObject buttonCanvas;

    // Start is called before the first frame update
    void Start()
    {
        buttonCanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            ResetScene();
        }
    }

    public void ResetScene()
    {
        // How to actually reset the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowCanvas()
    {
        buttonCanvas.gameObject.SetActive(true);
    }
}
