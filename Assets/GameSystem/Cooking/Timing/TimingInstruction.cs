using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingInstruction : MonoBehaviour
{
    public enum Type { None, Ingredient, Heat };
    public Type type = Type.None;
    public HeatLevel targetHeatLevel = HeatLevel.None;
    public float startTime;
    public float duration;
    public int points = 1;
    public string instruction = "";
}
