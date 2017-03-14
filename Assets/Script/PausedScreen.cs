using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedScreen : BaseScreen {

    GameObject previousScreen;

    public void SetPreviousScreen(GameObject ps)
    {
        previousScreen = ps;
    }

    public void ShowPreviousScreen()
    {
        if (previousScreen != null)
        {
            previousScreen.GetComponent<BaseScreen>().ActivateScreen(true);
            previousScreen = null;
        }
        else
        {
            GameManager.State = GameState.Play;
        }
    }
}
