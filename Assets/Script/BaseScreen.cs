using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseScreen : MonoBehaviour {

    public CanvasGroup cg;

    public void ActivateScreen(bool show)
    {
        gameObject.SetActive(true);
        if (show)
        {
            cg.alpha = 0f;
            StartCoroutine (Fade(0.05f));
        }
        else
        {
            cg.alpha = 1f;
            StartCoroutine(Fade(-0.05f));
        }        
    }
    IEnumerator Fade(float delta)
    {
        if (delta < 0)
        {
            while (cg.alpha > 0f)
            {
                cg.alpha += delta;
                yield return null;
            }
            cg.alpha = 0f;
            gameObject.SetActive(false);
        }
        else
        {
            while (cg.alpha < 1f)
            {
                cg.alpha += delta;
                yield return null;
            }
            cg.alpha = 1f;
        }

    }

}
