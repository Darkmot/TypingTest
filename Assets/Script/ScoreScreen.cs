using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : BaseScreen {
    public Text scoreText;
    public Text reachedText;
    public Text accuracyText;
    public Text streakText;

    public GameObject newBest;

    public new void ActivateScreen(bool show)
    {
        PlayerShip p = GameManager.Player;
        scoreText.text = p.score.ToString("D7");
        reachedText.text = "WAVE "+GameManager.Level.ToString("D3");
        accuracyText.text = ((float)p.totalHit / (float)p.totalShot).ToString("P2");
        streakText.text = p.streak.ToString("D6");

        int prevScore = PlayerPrefs.GetInt("BestScore", -1);
        if ((prevScore < 0) || ((prevScore >= 0) && (prevScore < p.score)))
        {
            newBest.SetActive(true);
            PlayerPrefs.SetInt("BestScore", p.score);
        }
        else
        {
            newBest.SetActive(false);
        }


        base.ActivateScreen(show);
    }
}
