using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedEnemy : BaseEnemy {
    public float minDelay;
    public float maxDelay;
    float fighterDelay;

    public new void InitEnemy(Vector2 newPos, float iSpeed)
    {
        base.InitEnemy(newPos, iSpeed);
        fighterDelay = Random.Range(minDelay,maxDelay);
    }
    new void Update()
    {
        base.Update();
        if (GameManager.State == GameState.Play)
        {
            fighterDelay -= Time.deltaTime;
            if (fighterDelay <= 0)
            {
                GameObject g = Instantiate(GameManager.EnemyFighterPrefab);
                g.transform.SetParent(GameManager.Player.gameplayScreen.transform, false);
                BaseEnemy enemy = g.GetComponent<BaseEnemy>();
                enemy.InitEnemy(new Vector2(rt.anchoredPosition.x, rt.anchoredPosition.y), speed+5);
                GameManager.AddEnemy(enemy);
                fighterDelay = Random.Range(minDelay, maxDelay);
             }
        }
    }
}
