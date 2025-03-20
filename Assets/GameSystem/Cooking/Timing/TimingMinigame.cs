using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingMinigame : MonoBehaviour
{
    public float startTime = 0.0f;
    public List<TimingInstruction> instructions = new List<TimingInstruction>();
    public int currentInstruction = 0;
    public int score = 0;
    public bool done = false;
    public Draggable stoveKnob;
    public HeatLevel heatLevel;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var instruction in GetComponentsInChildren<TimingInstruction>()) {
            instructions.Add(instruction);
            instruction.gameObject.SetActive(false);
        }
    }

    void OnEnable() {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        var instruction = instructions[currentInstruction];
        if (instruction.startTime <= Time.time) {
            if (instruction.startTime + instruction.duration <= Time.time) {
                NextInstruction();
            } else {
                instruction.gameObject.SetActive(true);
            }
        }   
    }

    public void NextInstruction() {
        if (currentInstruction + 1 >= instructions.Count) {
            done = true;
            Destroy(gameObject);
            return;
        }
        instructions[currentInstruction].gameObject.SetActive(false);
        currentInstruction++;
    }

    public void MarkInstructionAsCompleted() {
        if (done) return;
        NextInstruction();
        score++;
    }

    public void ChangeHeatLevel(HeatLevel heatLevel) {
        if (this.heatLevel == heatLevel)
            return;
        
        this.heatLevel = heatLevel;
        if (instructions[currentInstruction].type == TimingInstruction.Type.Heat && instructions[currentInstruction].targetHeatLevel == heatLevel) {
            MarkInstructionAsCompleted();
        } else {
            score--;
        }
    }
}
