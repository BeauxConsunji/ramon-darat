using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterScript : MonoBehaviour
{
    public PouringScript pouredWater;
    public PitcherScript pitcher;
    private float maxHeight = -2.3f;
    private float aimedHeight = 0.35f;
    private float moveSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 pos = transform.position;
        if (pos.y <= maxHeight)
        {
            pos.y += pouredWater.GetThickness() * moveSpeed * Time.fixedDeltaTime;
            //Debug.Log(pos.y);
        }
        else
            pos.y += 0;
        transform.position = pos;
    }

    //public float GetHeight()
    //    { return -1.32f + height; }

}
