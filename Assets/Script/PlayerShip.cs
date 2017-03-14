using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour {

    public GameObject bulletPrefab;
    public RectTransform rt;
    public GameplayScreen gameplayScreen;
    public ScoreScreen scoreScreen;

    public bool alive;
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
    
    }

    void PlayerReady()
    {
        GameManager.InitGame();       
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
