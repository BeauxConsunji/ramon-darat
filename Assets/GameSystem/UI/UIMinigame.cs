using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIMinigameState : UIState {
    public Minigame.Type type;
    public string loadingScreenText;
    public Sprite loadingScreenImage;
    public string gameObjectId;
}

public class UIMinigame : UIView<UITimingMinigameState>
{
    // TODO: Implement pause/reset menu
    void Start()
    {
        
    }
    

    public override void ApplyNewStateInternal() {

    }

    public void Pause() {
        // TODO: Implement pause minigame
    }

    public void Restart() {
        // TODO: Restart minigame, reset to beginning state of minigame
    }
    
}
