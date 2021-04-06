using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ShowControllers : MonoBehaviour
{
    public bool ShowController = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var hand in Player.instance.hands)
        {
            if (ShowController)
            {
                
            }
        }
    }
}
