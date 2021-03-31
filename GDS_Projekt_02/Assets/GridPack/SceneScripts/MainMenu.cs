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

   /* private IEnumerator SceneLoading(int sceneNumber)
    {
        Debug.Log("Działa"); 
        yield return new WaitForSeconds(delayTime);
        SceneManager.LoadScene(sceneNumber);
    }
    */
}
