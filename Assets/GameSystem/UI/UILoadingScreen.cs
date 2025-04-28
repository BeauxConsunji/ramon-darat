using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UILoadingScreen : UIView<UIRecipeState>
{
    public TMPro.TextMeshProUGUI instructions;
    public Image img;

    public override void ApplyNewStateInternal() {
        if (state.minigames.Count > 0) {
            instructions.text = state.minigames[state.currentMinigame].loadingScreenText;
            img.sprite = state.minigames[state.currentMinigame].loadingScreenImage;
        }
    }

    public void StartMinigame() {
        Debug.Log("Clicked");

        G.UI.uiType = state.minigames[state.currentMinigame].type == Minigame.Type.Timing ? UIType.TimingMinigame : UIType.HUD;
        if (G.I.TryGetGameObjectById(state.minigames[state.currentMinigame].gameObjectId, out var minigame)) {
            minigame.SetActive(true);
        }
        G.UI.MarkModified();
    }
}
