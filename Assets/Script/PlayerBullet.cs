using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed=200;
    BaseEnemy enemyTarget;

    public RectTransform rt;

	public void initBullet (BaseEnemy enemy) {

        enemyTarget = enemy;

        rt.anchoredPosition = GameManager.Player.rt.anchoredPosition;
        Vector2 enemyPos = enemyTarget.rt.anchoredPosition - rt.anchoredPosition;
        float angle = Mathf.Atan2(enemyPos.y, enemyPos.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rt.rotation = rotation;
		
	}
    public void Update()
    {
        rt.anchoredPosition = Vector2.MoveTowards(rt.anchoredPosition, enemyTarget.rt.anchoredPosition, speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject == enemyTarget.gameObject)
        {
            enemyTarget.Hit();
            Destroy(gameObject);
        }
    }
}
