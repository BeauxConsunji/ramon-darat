using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TimingMinigame : Minigame
{
    public float startTime = 0.0f;
    public List<TimingInstruction> instructions = new List<TimingInstruction>();
    public int currentInstruction = 0;
    public bool instructionIsActive = false;
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
                type = instruction.type
            });
        }
        G.UI.MarkModified();
    }

    void OnEnable() {
        InitializeUI();
        G.UI.timingMinigame.startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var instruction = instructions[currentInstruction];
        float instructionStart = startTime + instruction.startTime;
        float instructionEnd = instructionStart + instruction.duration;
        instruction.gameObject.SetActive(true);
        
        if (Time.time >= instructionStart && Time.time <= instructionEnd && !instructionIsActive) {
            instructionIsActive = true;
        }   else if (Time.time > instructionEnd) {
            instructionIsActive = false;
            NextInstruction();
        }
    }

    public void NextInstruction() {
        Debug.Log("Next Instruction");
        if (currentInstruction + 1 >= instructions.Count) {
            if (!instructionIsActive) {
                done = true;
                recipe.score = G.UI.timingMinigame.score;
                recipe.NextMinigame();
                gameObject.SetActive(false);
            }
            return;
        }
        instructionIsActive = false;
        instructions[currentInstruction].gameObject.SetActive(false);
        currentInstruction++;
    }

    public override void MarkCompleted(GameObject i=null) {
        if (done) return;
        
        if (instructionIsActive) {
            Debug.Log("MarkCompleted");
            G.UI.timingMinigame.score++;
            G.UI.timingMinigame.MarkModified();
            NextInstruction();
        }
        
    }

    public void ChangeHeatLevel(HeatLevel heatLevel) {
        if (this.heatLevel == heatLevel)
            return;
        Debug.Log("Set Heat Level to " + heatLevel.ToString());
        var instruction = instructions[currentInstruction];
        this.heatLevel = heatLevel;
        if (instruction.type == TimingInstruction.Type.Heat && instruction.targetHeatLevel == heatLevel) {
            MarkCompleted();
        }
    }
}
