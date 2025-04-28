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
        instructions.text = state.loadingScreenInstruction;
        img.sprite = state.loadingScreenImage;
    }

    public void StartRecipe() {
        Debug.Log("Clicked");
        G.UI.uiType = UIType.TimingMinigame; // temporary
        G.UI.MarkModified();
        if (G.I.TryGetGameObjectById(state.gameObjectId, out var recipe)) {
            recipe.SetActive(true);
        }
    }
}
