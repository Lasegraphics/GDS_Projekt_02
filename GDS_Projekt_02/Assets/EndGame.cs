using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public int blueTeamScore = 1;
    public int redTeamScore = 1;
    bool endGame = true;
    ScoreController scoreController;

    private void Awake()
    {
        scoreController = FindObjectOfType<ScoreController>();
        DontDestroyOnLoad(this.gameObject);
    }
    public void StartGame()
    {
        blueTeamScore = scoreController.scoreBlueTeam;
        redTeamScore = scoreController.scoreRedTeam;
        endGame = false;
    }


    void Update()
    {
        if (endGame == false)
        {
            blueTeamScore = scoreController.scoreBlueTeam;
            redTeamScore = scoreController.scoreRedTeam;
            if (blueTeamScore <= 0)
            {
                SceneManager.LoadScene("ENDGAME");
                endGame = true;
            }
            if (redTeamScore <= 0)
            {
                SceneManager.LoadScene("ENDGAME");
                endGame = true;
            }

        }
    }
}
