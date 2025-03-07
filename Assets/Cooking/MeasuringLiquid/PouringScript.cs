using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringScript : MonoBehaviour
{
    public PitcherScript pitcher;
    private float maxHeight = 1.65f;
    private float height;
    private float maxThickness = 0.05f;
    private float thickness;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(pitcher.getZ() >= 10 && pitcher.isPouring())
        {
            if (thickness >= maxThickness)
                thickness = maxThickness;
            else if(thickness < maxThickness)
            {
                thickness += Time.deltaTime * 0.5f;
            }
            if (height >= maxHeight)
                height = maxHeight;
            if(height < maxHeight)
            {
                height += Time.deltaTime * 15f;
            }
        }
        else if(pitcher.getZ() <= 20 && !pitcher.isPouring())
        {
            if (thickness < 0)
            {
                thickness = 0;
                height = 0;
            }
            else if (thickness > 0)
            {
                thickness -= Time.deltaTime * 0.1f;
            }

            //if (height < 0)
            //    height = 0;
            //else if (height > 0)
            //{
            //    height -= Time.deltaTime * 15f;
            //}

        }

        transform.localScale = new Vector3(thickness, height, 1);
    }
}
