using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemy : BaseEnemy {
    public float minDelay;
    public float maxDelay;
    public int bulletCount;

    float bulletDelay;

    public new void InitEnemy(Vector2 newPos, float iSpeed)
    {
        base.InitEnemy(newPos, iSpeed);
        bulletDelay = Random.Range(minDelay, maxDelay);
    }
    new void Update()
    {
        base.Update();
        if (GameManager.State == GameState.Play)
        {
            bulletDelay -= Time.deltaTime;
            if (bulletDelay <= 0)
            {
                for (int i = 0; i < bulletCount; i++)
                {
                    GameObject g = Instantiate(GameManager.EnemyBulletPrefab);
                    g.transform.SetParent(GameManager.Player.gameplayScreen.transform, false);
                    BulletEnemy enemy = g.GetComponent<BulletEnemy>();
                    enemy.InitEnemy(new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y), speed + 5, 
                        enemyImage.localEulerAngles.z + 120 + (i*(120/bulletCount)) );
                    GameManager.AddEnemy(enemy);
                }
                bulletDelay = Random.Range(minDelay, maxDelay);
            }
        }
    }
}
