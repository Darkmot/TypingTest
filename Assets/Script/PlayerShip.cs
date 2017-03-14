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
    public int score;
    public int scoreModifier;
    public bool alive;
    public int empCount;
    
    void OnEnable()
    {
        alive = true;
        score = 0;
        scoreModifier = 1;
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
