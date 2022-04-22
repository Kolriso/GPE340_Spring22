using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public Image splashImage;
    public string loadLevel;
    
    // Start is called before the first frame update
    IEnumerator Start()
    {
        splashImage.canvasRenderer.SetAlpha(0.0f); // Sets the alpha for the splash screen
        // This is how you get the splash screen to fade in and fade out
        FadeIn();
        yield return new WaitForSeconds(2.5f);
        FadeOut();
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(loadLevel);
    }

    public void FadeIn()
    {
        splashImage.CrossFadeAlpha(1.0f, 1.5f, false);
    }

    public void FadeOut()
    {
        splashImage.CrossFadeAlpha(0.0f, 2.5f, false);
    }
}
