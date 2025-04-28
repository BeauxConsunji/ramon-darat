using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIRecipeState : UIState {
    // info abt 
    public bool locked = true;
    public string name;
    public string description;
    public Sprite thumbnail;
}
public class UIRecipe : UIView<UIRecipeState>
{
    public TMPro.TextMeshProUGUI name;

    // TODO: sprite for locked
    public override void ApplyNewStateInternal() {
        name.text = state.name;

        // TODO: lockedSprite.SetActive(state.locked);
    }

    public void SelectRecipe() {
        Debug.Log("Clicked");
        G.UI.uiType = UIType.HUD;
        G.UI.MarkModified();
    }
}
