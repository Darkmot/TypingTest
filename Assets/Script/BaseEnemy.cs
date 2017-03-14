﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseEnemy : MonoBehaviour {

    public int minWord;
    public int maxWord;
    public float speed = 40f;

    public RectTransform enemyImage;
    public Text textTarget;
    public Image textContainer;

    public RectTransform rt;
    public string wordToHit;
    int life;

    public void InitEnemy()
    {
        ResizeWord(GameManager.TextDB.GetRandomWord(Random.Range(minWord,maxWord+1)));
        life = wordToHit.Length;

        rt.anchoredPosition = new Vector2(Random.Range(10, 320), Random.Range(640, 720));
        Vector2 playerPos = GameManager.Player.GetComponent<RectTransform>().anchoredPosition - rt.anchoredPosition;
        float angle = Mathf.Atan2(playerPos.y,playerPos.x) * Mathf.Rad2Deg - 90f;
        Quaternion rotation = Quaternion.AngleAxis(angle,Vector3.forward);
        enemyImage.rotation = rotation;

    }

    public void ResizeWord(string newWord)
    {
        wordToHit = newWord;
        textTarget.text = wordToHit;
        textContainer.GetComponent<RectTransform>().sizeDelta = new Vector2((wordToHit.Length * 10) + 10, 20);

        if (wordToHit.Length <= 0)
        {
            textContainer.gameObject.SetActive(false);
        }
    }

    public void SetSelected(bool select)
    {
        if (select)
        {
            textTarget.color = new Color(1f,0.9f,0f) ;
            textContainer.color = new Color(0.2f, 0.2f, 0.1f);
        }
        else
        {
            textTarget.color = Color.white;
            textContainer.color = Color.black;

        }
    }

    public void Update()
    {
        rt.anchoredPosition = Vector2.MoveTowards(rt.anchoredPosition,GameManager.Player.GetComponent<RectTransform>().anchoredPosition,speed * Time.deltaTime);
    }

    public void Hit()
    {
        life--;
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
