using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {

    public float speed=200;
    BaseEnemy enemyTarget;

    public RectTransform rt;
    public Animator anim;
    bool stop;
	public void initBullet (BaseEnemy enemy) {

        enemyTarget = enemy;

        rt.anchoredPosition = GameManager.Player.rt.anchoredPosition;
        Vector2 enemyPos = enemyTarget.rt.anchoredPosition - rt.anchoredPosition;
        float angle = Mathf.Atan2(enemyPos.y, enemyPos.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        rt.rotation = rotation;
        stop = false;
	}
    void Update()
    {
        if (enemyTarget != null)
        {
            if ((GameManager.State == GameState.Play) && (!stop))
                rt.anchoredPosition = Vector2.MoveTowards(rt.anchoredPosition, enemyTarget.rt.anchoredPosition, speed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject == enemyTarget.gameObject)
        {
            enemyTarget.Hit();
            stop = true;
            anim.SetTrigger("Blast");
            transform.SetAsLastSibling();
        }
    }
    void OnBulletFinished()
    {
        Destroy(gameObject);
    }
}
