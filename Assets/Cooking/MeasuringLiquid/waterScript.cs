using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterScript : MonoBehaviour
{
    public PouringScript pouredWater;
    //public PitcherScript pitcher;
    private float maxHeight = 0.5f;
    private float aimedHeight = 0.35f;
    private float height;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (height <= maxHeight)
        {
            height += pouredWater.getThickness() * 0.005f;
        }
        else
            height = 0;

        transform.localScale = new Vector3(0.27f, height, 1);


    }

    public float getHeight()
        { return -1.32f + height; }
   
}
