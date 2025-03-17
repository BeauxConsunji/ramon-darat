using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingMinigame : MonoBehaviour
{
    public float startTime = 0.0f;
    public List<TimingInstruction> = new List<TimingMinigame>();
    public int instructionIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable() {
        startTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
