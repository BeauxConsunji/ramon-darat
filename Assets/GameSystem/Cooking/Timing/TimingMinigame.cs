using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingMinigame : MonoBehaviour
{
    public float startTime = 0.0f;
    public List<TimingInstruction> instructions = new List<TimingInstruction>();
    public int currentInstruction = 0;
    public bool done = false;
    public Draggable stoveKnob;
    public HeatLevel heatLevel;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        foreach (var instruction in GetComponentsInChildren<TimingInstruction>()) {
            instructions.Add(instruction);
            instruction.gameObject.SetActive(false);
        }
        InitializeUI();
    }
    void InitializeUI() {
        G.UI.timingMinigame.instructions.Clear();

        foreach (var instruction in instructions) {
            G.UI.timingMinigame.AddInstruction(new UITimingInstructionState() {
                startTime = instruction.startTime,
                duration = instruction.duration,
                instruction = instruction.instruction,

            });
        }
        G.UI.MarkModified();
    }

    void OnEnable() {
        startTime = Time.time;
        G.UI.timingMinigame.startTime = startTime;
        InitializeUI();
    }

    // Update is called once per frame
    void Update()
    {
        var instruction = instructions[currentInstruction];
        if (startTime + instruction.startTime <= Time.time) {
            if (startTime + instruction.startTime + instruction.duration <= Time.time) {
                NextInstruction();
            } else {
                instruction.gameObject.SetActive(true);
            }
        }   
    }

    public void NextInstruction() {
        if (currentInstruction + 1 >= instructions.Count) {
            done = true;
            gameObject.SetActive(false);
            return;
        }
        instructions[currentInstruction].gameObject.SetActive(false);
        currentInstruction++;
    }

    public void MarkInstructionAsCompleted() {
        if (done) return;
        NextInstruction();
        // G.UI.timingMinigame.instructions[currentInstruction].done = true;
        // G.UI.timingMinigame.instructions[currentInstruction].MarkModified();
        
        G.UI.timingMinigame.score++;
        G.UI.timingMinigame.MarkModified();
    }

    public void ChangeHeatLevel(HeatLevel heatLevel) {
        if (this.heatLevel == heatLevel)
            return;
        
        this.heatLevel = heatLevel;
        if (instructions[currentInstruction].type == TimingInstruction.Type.Heat && instructions[currentInstruction].targetHeatLevel == heatLevel) {
            MarkInstructionAsCompleted();
        } else {
            G.UI.timingMinigame.score--;
            G.UI.timingMinigame.MarkModified();
        }
    }
}
