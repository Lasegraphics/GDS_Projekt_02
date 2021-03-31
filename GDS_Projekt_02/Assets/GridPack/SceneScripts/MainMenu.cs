using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
public class MainMenu : MonoBehaviour
{
  public void PlayThat()
    {
        
         SceneManager.LoadScene(0);
    }

    public void Quit()
    {
         SceneManager.LoadScene(2);
    }

    public void Back()
    {
        SceneManager.LoadScene(1);
    }

    public void Yes()
    {
        Application.Quit(); 
    }

    public void Credits()
    {
        SceneManager.LoadScene(3);
    }

    public void Settings()
    {
        SceneManager.LoadScene(4);
    }

    public void ToMusic()
    {
        SceneManager.LoadScene(5);
    }

    public void SetFullScreen(bool IsSet)
    {
        Screen.fullScreen = IsSet; 
    }

    public void SetDesignerMode(bool IsEnable)
    {
        
    }

    

   /* private IEnumerator SceneLoading(int sceneNumber)
    {
        Debug.Log("Działa"); 
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneNumber);
    }
    */
}
