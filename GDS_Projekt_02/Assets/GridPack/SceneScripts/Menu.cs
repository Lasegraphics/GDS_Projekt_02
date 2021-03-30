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
    public static bool IsPaused; 
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
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);   
        Time.timeScale = 0f; 
        IsPaused = true; 
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
    }

    public void Back()
    {
        SettingsObject.SetActive(false); 
        pauseMenu.SetActive(true); 
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
}
