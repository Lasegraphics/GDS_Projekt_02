using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using GridPack.Grid;
using GridPack.SceneScripts; 
public class Menu : MonoBehaviour
{
   
   
    
   
    public void PlayAgain()
    {
        SceneManager.LoadScene("MainGame");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetFullScreen(bool IsSet)
    {
        Screen.fullScreen = IsSet; 
    }
}
