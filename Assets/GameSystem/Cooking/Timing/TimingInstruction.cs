using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimingInstruction : MonoBehaviour
{
    public enum Type { Ingredient, Heat };
    public Draggable.KnobSetting targetHeatLevel = HeatLevel.None;
    public float timeStart;
    public float duration;
    public int points;
}
