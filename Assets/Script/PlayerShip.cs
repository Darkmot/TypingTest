using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShip : MonoBehaviour {

    public GameObject bulletPrefab;
    public RectTransform rt;
    public GameplayScreen gameplayScreen;
    public ScoreScreen scoreScreen;

    public PlayerEMP playerEMP;
    public Text empText;
    public Image empIcon;

    public Slider comboSlider;
    public Animator[] animX;

    public int[] comboLimit;

    public int score;
    public int scoreModifier;
    public bool alive;
    public int empCount;

    public int combo;
    public int streak;

    public int totalHit;
    public int totalShot;
    
    void OnEnable()
    {
        alive = true;
        score = 0;
        scoreModifier = 1;
        combo = 0;
        comboSlider.value = 0;
        streak = 0;
        totalHit = 0;
        totalShot = 0;
        rt.localRotation = Quaternion.identity;
        empCount = 3;
        UpdateEMP();
    }
    void UpdateEMP()
    {
        empText.text = "" + empCount;
        if (empCount <= 0)
            empIcon.color = new Color(1f,1f,1f,0.5f);
        else
            empIcon.color = Color.white;
    }
    public void ShotEnemy(BaseEnemy target)
    {
        GameObject g = Instantiate(bulletPrefab);
        g.transform.SetParent(GameManager.GameScreen);
        g.transform.SetAsFirstSibling();
        PlayerBullet b = g.GetComponent<PlayerBullet>();
        b.initBullet(target);

        Vector2 enemyPos = target.rt.anchoredPosition - rt.anchoredPosition;
        float angle = Mathf.Atan2(enemyPos.y, enemyPos.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rt.rotation = rotation;

        score += scoreModifier;
        totalHit++;
    }
    public void BlastEMP()
    {
        if ((!playerEMP.GetComponent<Image>().enabled) && (empCount > 0))
        {
            empCount--;
            UpdateEMP();
            playerEMP.Blast();
        }
    }
    public void AddCombo(bool isCombo)
    {
        if (isCombo)
        {
            combo++;
            if (combo > streak)
                streak = combo;
            if (scoreModifier < 4)
            {
                int prevLimit = 0;
                for (int i = 0; i < scoreModifier-1; i++)
                    prevLimit += comboLimit[i];
                comboSlider.value = ((scoreModifier - 1) * 30) + ((30f / (float)comboLimit[scoreModifier]) * (combo-prevLimit));
                if (combo > prevLimit + comboLimit[scoreModifier-1] + comboLimit[scoreModifier])
                {
                    scoreModifier++;
                    animX[scoreModifier - 2].SetTrigger("Show");
                }
            }
        }
        else
        {
            combo = 0;
            comboSlider.value = 0;
            scoreModifier = 1;
        }
        totalShot++;
    }

    void PlayerReady()
    {
        GameManager.InitLevel();       
    }

    void OnPlayerFinished()
    {
        gameplayScreen.ActivateScreen(false);
        scoreScreen.ActivateScreen(true);
        GameManager.ClearEnemyList();
        GameManager.State = GameState.Score;
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy")
        {
            alive = false;
            GetComponent<Animator>().SetTrigger("Blast");
            transform.SetAsLastSibling();
        }
    }

}
