using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreen : BaseScreen {
    public new void ActivateScreen(bool show)
    {
        if (show)
            GameManager.State = GameState.Start;
        base.ActivateScreen(show);
    }

    public void PauseGame()
    {
        GameManager.State = GameState.Paused;
    }

    public void ClearEnemySelection()
    {
        GameManager.ClearCurrentEnemy();
    }
}
