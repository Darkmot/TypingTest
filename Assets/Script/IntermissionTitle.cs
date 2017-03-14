using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntermissionTitle : MonoBehaviour {

    public Text waveText;
    public Text scoreText;

	public void StartIntermission (int wave, int score) {
        waveText.text = "WAVE " + wave.ToString("D3");
        scoreText.text = "SCORE " + score.ToString("D7");
        GetComponent<CanvasGroup>().alpha = 0f;
        gameObject.SetActive(true);
    }
	
	void EndIntermission () {
        gameObject.SetActive(false);
        GameManager.InitNextLevel();
	}
}
