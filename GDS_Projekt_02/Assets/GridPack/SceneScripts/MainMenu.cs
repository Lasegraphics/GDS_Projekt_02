using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class MainMenu : MonoBehaviour
{
  public void PlayThat()
    {
        
         SceneManager.LoadScene(1);
    }
    public void GoToCreditsMusic()
    {
        SceneManager.LoadScene("CreditsMusic");
    }
    public void GoToAreYouSure()
    {
         SceneManager.LoadScene("AreYouSure");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Yes()
    {
        Application.Quit(); 
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToSettings()
    {
        SceneManager.LoadScene("Settigns");
    }

    public void GoToMusic()
    {
        SceneManager.LoadScene("CreditsMusic");
    }

    public void SetFullScreen(bool IsSet)
    {
        Screen.fullScreen = IsSet; 
    }
}
