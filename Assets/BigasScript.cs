using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigasScript : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canGetRice;

    // don't allow pouring unless pitcher is in a pouring area
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Layer 9 = RiceCup
        if (collision.gameObject.layer == 9)
        {
            canGetRice = true;
            Debug.Log("KUHA KA NA NG KANIN");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Layer 9 = RiceCup
        if (collision.gameObject.layer == 9)
        {
            canGetRice = false;
        }
    }

    public bool CanGetRiceChecker()
    {
        if (canGetRice)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}