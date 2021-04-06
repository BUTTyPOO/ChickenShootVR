using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailManager : MonoBehaviour
{
    [SerializeField] bool startSessionOnSpawn;
    [SerializeField] GameObject[] railObjs;
    ArenaRail[] rails;

    public delegate void SessionOverEventHandler();
    public event SessionOverEventHandler SessionOverEvent;

    public delegate void SessionStartedEventHandler();
    public event SessionStartedEventHandler SessionStartedEvent;


    void Start()
    {
        InitRailArray();
        if (startSessionOnSpawn)
            StartSession();
    }

    void InitRailArray()
    {
        rails = new ArenaRail[railObjs.Length];
        for (int i = 0; i < railObjs.Length; ++i)
        {
            rails[i] = railObjs[i].GetComponent<ArenaRail>();
            rails[i].railOverEvent += OnRailOver;
        }
    }

    public void StartSession()
    {
        SessionStartedEvent?.Invoke();
        foreach(ArenaRail rail in rails)
        {
            rail.BeginSession();
        }
    }

    void OnRailOver()   // When rail stops spawning actors and ends
    {
        int count = 0;
        for (int i = 0; i < rails.Length; ++i)
        {
            if (!rails[i].playing)
                ++count;
        }

        if (count == rails.Length)
            Invoke("OnSessionOver", 5.0f);
    }

    void OnSessionOver()
    {
        SessionOverEvent?.Invoke();
    }
}
