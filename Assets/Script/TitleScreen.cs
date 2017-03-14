using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : BaseScreen {
    public new void ActivateScreen(bool show)
    {
        if (show)
            GameManager.State = GameState.Title;
        base.ActivateScreen(show);
    }

}
