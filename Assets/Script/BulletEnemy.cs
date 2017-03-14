using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : BaseEnemy {

    Rect rectCheck;

    public new void InitEnemy(Vector2 newPos, float iSpeed,float angle)
    {
        base.InitEnemy(newPos,iSpeed);
        enemyImage.Rotate(new Vector3(0f, 0f, angle));
        ResizeWord(GameManager.GetLetter(Random.Range(0, 26)));
        life = 1;
        rectCheck = new Rect(-20, -20, 380, 640);
    }

    new void Update()
    {
        if (GameManager.State == GameState.Play)
        {
            Vector2 dir = (Vector2)(Quaternion.AngleAxis(enemyImage.eulerAngles.z, Vector3.forward) * Vector3.up);
            if (rectCheck.Contains(rt.anchoredPosition))
            {
                GetComponent<Rigidbody2D>().AddForce(dir * speed);
            }
            else
            {
                GameManager.RemoveEnemy(this);
            }

        }
    }

}
