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

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit(); 
    }

    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void GoToMusic()
    {
        SceneManager.LoadScene("CreditsMusic");
    }

    public void SetFullScreen(bool IsSet)
    {
        Screen.fullScreen = IsSet; 
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
}
