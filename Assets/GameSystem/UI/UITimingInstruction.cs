using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UITimingInstructionState : UIState {
    public float startTime;
    public float duration;

    public string instruction;
    public float pixelsPerSecond;
    public bool done = false;
    public TimingInstruction.Type type = TimingInstruction.Type.None;
    public int points = 1;
}

public class UITimingInstruction : UIView<UITimingInstructionState>
{
    public TMPro.TextMeshProUGUI instruction;
    public UITimingMinigame minigame;
    public UnityEngine.UI.Image image;
    private RectTransform rectTransform;
    public Color defaultColor, ingredientInstructionColor, heatInstructionColor;

    void Start()
    {
        if (minigame == null)
            minigame = FindObjectOfType<UITimingMinigame>();
        if (image == null)
            image = GetComponent<UnityEngine.UI.Image>();
        rectTransform = GetComponent<RectTransform>();
        if (minigame != null) {
            Debug.Log(minigame.pixelsPerSecond);

            var width = state.duration * minigame.pixelsPerSecond;
            rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        }
    }
    void Update()
    {
        rectTransform.anchoredPosition = new Vector2(minigame.offset + (state.startTime + minigame.state.startTime - Time.time) * minigame.pixelsPerSecond, 0.0f); 
    }

    public override void ApplyNewStateInternal() {
        instruction.text = state.instruction;
        if (state.type == TimingInstruction.Type.Heat)
            image.color = heatInstructionColor;
        else if (state.type == TimingInstruction.Type.Ingredient)
            image.color = ingredientInstructionColor;
        else
            image.color = defaultColor;

        if (state.done) {
            Destroy(gameObject);
        }
    }
}
