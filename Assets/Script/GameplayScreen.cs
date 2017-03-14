using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayScreen : BaseScreen {
    public IntermissionTitle intermission;
    public PausedScreen pausedScreen;

    public new void ActivateScreen(bool show)
    {
        if (show)
            GameManager.State = GameState.Start;
        base.ActivateScreen(show);
    }

    public void PauseGame()
    {
        if (GameManager.State == GameState.Play)
        {
            pausedScreen.ActivateScreen(true);
            GameManager.State = GameState.Paused;
        }
    }

    public void ClearEnemySelection()
    {
        GameManager.ClearCurrentEnemy();
    }
}
