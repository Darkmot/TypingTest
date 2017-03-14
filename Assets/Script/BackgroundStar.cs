using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundStar : MonoBehaviour {
    
    public int speed = 100;
    public int limitY = 400;

    RectTransform rt;
    void Start()
    {
        rt = (RectTransform)gameObject.transform;
    }

	void Update () {
        if (GameManager.State != GameState.Paused)
        {
            float newY = rt.anchoredPosition.y - ((float)speed * Time.deltaTime);
            if (newY < -limitY)
                newY += limitY;
            Vector2 newPos = new Vector2(rt.anchoredPosition.x, newY);
            rt.anchoredPosition = newPos;
        }
	}
}
