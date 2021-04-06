using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTxt : MonoBehaviour
{
    Text txt;
    float timeLeft = 50;
    bool countDownInProgress = false;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<Text>();
    }

    public void StartClock()
    {
        if (!countDownInProgress)
            StartCoroutine(CountDown());
    }
    
    IEnumerator CountDown()
    {
        countDownInProgress = true;
        while(timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            UpdateTxt();
            yield return null;
        }
        countDownInProgress = false;
        timeLeft = 0;
        UpdateTxt();
        timeLeft = 50;
    }

    void UpdateTxt()
    {
        txt.text = timeLeft.ToString("F2");
    }
}
