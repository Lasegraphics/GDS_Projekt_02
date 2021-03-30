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
    public static bool IsPaused; 
    public MyOtherHexagon Markoff; 
    
    void Start()
    {
        pauseMenu.SetActive(false);  
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
