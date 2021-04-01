using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using GridPack.Grid;
using GridPack.SceneScripts; 
public class Menu : MonoBehaviour
{
    public GameObject pauseMenu; 
    public GameObject QuitMenu; 
    public GameObject SettingsObject; 
    public GameObject MusicMenu; 
    public static bool IsPaused; 
    public static bool IsSettings; 
    private MyOtherHexagon Markoff; 
    
    void Start()
    {
        pauseMenu.SetActive(false);  
        Markoff = gameObject.GetComponent<MyOtherHexagon>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(IsSettings == false)
            {
                if(IsPaused)
                {
                    ResumeGame();
                }
                else 
                {
                    PauseGame();
                    //Markoff.UnMark();
                }
            }
            else 
            {
                Debug.Log("Jesteś w ustawieniach");
            }
                
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);   
        Time.timeScale = 0f; 
        IsPaused = true; 
         IsSettings = false;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false); 
        Time.timeScale = 1f; 
        IsPaused = false; 
    }

    public void ToSettings()
    {
        SettingsObject.SetActive(true); 
        pauseMenu.SetActive(false); 
        IsSettings = true;
        
    }

    public void ToMusicMenu()
    {
        MusicMenu.SetActive(true);
        SettingsObject.SetActive(false);
    }

    public void BackToSettings()
    {
        SettingsObject.SetActive(true);
        MusicMenu.SetActive(false);
    }

    public void Back()
    {
        SettingsObject.SetActive(false); 
        pauseMenu.SetActive(true); 
        IsSettings = false;
    }

    public void ToMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
         QuitMenu.SetActive(true); 
         pauseMenu.SetActive(false); 
    }

    public void No()
    {
        QuitMenu.SetActive(false); 
        pauseMenu.SetActive(true); 
    }

    public void Yes()
    {
        Application.Quit();
    }

    public void SetFullScreen(bool IsSet)
    {
        Screen.fullScreen = IsSet; 
    }

    public void SetDesignerMode(bool IsEnable)
    {
        
    }
}
