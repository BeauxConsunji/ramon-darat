using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouringAreaScript : MonoBehaviour
{
    private bool canPour;

    // don't allow pouring unless pitcher is in a pouring area
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Layer 7 = Pitcher
        if (collision.gameObject.layer == 7)
        {
            canPour = true;
            Debug.Log("You can pour now baby");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Layer 7 = Pitcher
        if (collision.gameObject.layer == 7)
        {
            canPour = false;
            Debug.Log("CAN'T POUR ANYMORE BOZO");
        }
    }

    public bool CanPourChecker()
    {
        if (canPour)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
