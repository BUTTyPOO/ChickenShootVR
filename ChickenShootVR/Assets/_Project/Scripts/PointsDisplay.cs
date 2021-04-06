using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsDisplay : MonoBehaviour
{
    [SerializeField] Text pointsTxt;
    public int points = 0;
    public static PointsDisplay instance;
    RailManager railMan;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        pointsTxt.text = "0";
    }

    void OnEnable()
    {
        railMan = GameObject.Find("RailManager").GetComponent<RailManager>();
        railMan.SessionStartedEvent += ResetPoints;
    }

    void OnDisable()
    {
        railMan.SessionStartedEvent -= ResetPoints;
    }

    public void AddPoints(int gainedPoints)    //Adds points to display
    {
        points += gainedPoints;
        UpdatePointsText();
    }

    void UpdatePointsText()
    {
        pointsTxt.text = points.ToString();
    }

    void ResetPoints()
    {
        points = 0;
        UpdatePointsText();
    }
}
