using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UITimingMinigameState : UIState {
    public List<UITimingInstructionState> instructions = new List<UITimingInstructionState>();
    public int score;
    public float startTime;
    public void AddInstruction(UITimingInstructionState instruction) {
        instructions.Add(instruction);
        MarkModified();
    }
}

public class UITimingMinigame : UIView<UITimingMinigameState>
{
    public TMPro.TextMeshProUGUI score;
    public GameObject instructionPrefab;
    public RectTransform currentTimeLine;
    public RectTransform timeline;
    public RectTransform rectTransform;

    public float offset = 50.0f; // offset from the left which indicates the current time
    public float widthInSeconds = 30.0f; // the width of the timeline is equivalent to how many seconds
    public float pixelsPerSecond { get { return G.I.GetAbsoluteSize(rectTransform).x / widthInSeconds; }}

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log(pixelsPerSecond);
        currentTimeLine.anchoredPosition += new Vector2(offset, 0.0f);
    }
    

    public override void ApplyNewStateInternal() {
        score.text = "Score: " + state.score;
        ApplyNewStateArray<UITimingInstructionState, UITimingInstruction>(timeline, instructionPrefab, state.instructions);
    }

    
}
