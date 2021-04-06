using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTxt : MonoBehaviour
{
    Text txt;
    RailManager railMan;
    PointsDisplay pointsTxt;
    int highScore = 0;

    public delegate void NewHighScoreEventHandler();
    public event NewHighScoreEventHandler NewHighScoreEvent;

    void OnEnable()
    {
        txt = GetComponent<Text>();
        pointsTxt = GameObject.Find("PointsCanvas").GetComponent<PointsDisplay>();

        railMan = GameObject.Find("RailManager").GetComponent<RailManager>();
        railMan.SessionOverEvent += UpdateHighScore;

        UpdateHighScore();
    }

    void OnDisable()
    {
        railMan.SessionOverEvent -= UpdateHighScore;
    }

    void UpdateHighScore()
    {
        if (pointsTxt.points > highScore)
        {
            highScore = pointsTxt.points;
            NewHighScoreEvent?.Invoke();
        }
        txt.text = highScore.ToString();
    }
}
