using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystemSpawner : MonoBehaviour
{
    public GameObject gameSystem;

    void Awake()
    {
        Instantiate(gameSystem);   
    }
}
