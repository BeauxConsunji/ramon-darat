using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class SaveData {}

public class GameObjectID : MonoBehaviour
{
    public string[] ids;
    public bool activeOnAwake = true;
    void Awake() {
        foreach (var id in ids)
        {
            if (!G.I.gameObjectRegistry.ContainsKey(id))
            {
                G.I.RegisterGameObject(id, gameObject);
            }
        }
        if (!activeOnAwake)
            gameObject.SetActive(false);
    }
    
    [HideInInspector]

    private void OnDestroy()
    {
        // unregister
        if (G.I != null)
        {
            foreach (var id in ids)
            {
                if (G.I.gameObjectRegistry.ContainsKey(id)) G.I.UnregisterGameObject(id, gameObject);
            }
        }
    }
}
