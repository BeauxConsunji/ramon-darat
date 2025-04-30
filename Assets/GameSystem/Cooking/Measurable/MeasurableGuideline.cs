using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideScript : MonoBehaviour
{
    public Minigame minigame;
    public float timeEnter = -1;
    public float duration = 2.0f; // how many seconds to wait before marked as success
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (minigame != null && timeEnter != -1  && Time.time - timeEnter >= duration) {
            minigame.MarkCompleted();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MeasurableIngredient") {
            timeEnter = Time.time;
            Debug.Log("MeasurableIngredient Entered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MeasurableIngredient")
        {
            Debug.Log("MeasurableIngredient Failed");
        }
    }
}