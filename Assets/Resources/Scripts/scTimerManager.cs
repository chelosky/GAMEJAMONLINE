using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scTimerManager : MonoBehaviour
{
    private Text gameTimerText;
    private float countDown = 15;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.name == "TimerText")
            {
                gameTimerText = child.GetComponent<Text>();
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        countDown -= Time.deltaTime;

        int seconds = (int)(countDown);

        string timerText = string.Format("Timer : {0}", seconds);

        gameTimerText.text = timerText;

        if(countDown < 0)
        {
            // lose
        }
    }
}
