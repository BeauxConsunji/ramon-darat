using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UITimingInstructionState : UIState {
    public float startTime;
    public float duration;

    public string instruction;
    public float pixelsPerSecond;
    public bool done = false;
}

public class UITimingInstruction : UIView<UITimingInstructionState>
{
    public TMPro.TextMeshProUGUI instruction;
    public UITimingMinigame minigame;
    private RectTransform rectTransform;

    void Start()
    {
        if (minigame == null)
            minigame = FindObjectOfType<UITimingMinigame>();

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
        if (state.done) {
            Destroy(gameObject);
        }
    }
}
