using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class UITimingMinigameState : UIMinigameState {
    public List<UITimingInstructionState> instructions = new List<UITimingInstructionState>();
    public int score;
    public int total;
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

    public float offset = 100.0f; // offset from the left which indicates the current time
    public float widthInSeconds = 8.0f; // the width of the timeline is equivalent to how many seconds
    public float pixelsPerSecond;

    void Start()
    {
        Debug.Log(pixelsPerSecond);
        foreach (var instruction in state.instructions) {
            state.total += instruction.points;
        }
        // currentTimeLine.anchoredPosition = new Vector2(offset, 0.0f);
        pixelsPerSecond = timeline.rect.width / widthInSeconds;
    }
    
    
    public override void ApplyNewStateInternal() {
        // score.text = "Score: " + G.UI.timingMinigame.score;
        ApplyNewStateArray<UITimingInstructionState, UITimingInstruction>(timeline, instructionPrefab, state.instructions);
    }

    
}
