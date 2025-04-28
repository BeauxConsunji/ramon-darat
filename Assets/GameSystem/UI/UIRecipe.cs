using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UIRecipeState : UIState {
    // info abt 
    public bool locked = true;
    public string name;
    public string description;
    public Sprite thumbnail;
    public string gameObjectId;
    public Sprite loadingScreenImage;
    public string loadingScreenInstruction;
}
public class UIRecipe : UIView<UIRecipeState>
{
    public TMPro.TextMeshProUGUI name;
    public TMPro.TextMeshProUGUI description;
    public Image img;

    // TODO: sprite for locked
    public override void ApplyNewStateInternal() {
        name.text = state.name;
        description.text = state.description;
        img.sprite = state.thumbnail;

        // TODO: lockedSprite.SetActive(state.locked);
    }

    public void SelectRecipe() {
        Debug.Log("Clicked");
        G.UI.uiType = UIType.LoadingScreen;
        G.UI.loadingScreen = state;
        G.UI.loadingScreen.MarkModified();
        G.UI.MarkModified();
    }
}
