using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour {

    public void StartGame() {
        G.UI.uiType = UIType.RecipeSelector;
        G.UI.MarkModified();
        Debug.Log("StartGame");
    }
}
