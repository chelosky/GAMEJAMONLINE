using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scTimerManager : MonoBehaviour
{
    private Text gameTimerText;
    private float countDown;
    // Start is called before the first frame update
    void Start()
    {
        countDown = 15;
        foreach (Transform child in transform)
        {
            if(child.name == "TimerText")
            {
                gameTimerText = child.GetComponent<Text>();
                break;
            }
        }
    }

    public void RestartCounter()
    {
        countDown = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if(scGameManager.instance.stateGame == 1)
        {
            countDown -= Time.deltaTime;

            int seconds = (int)(countDown);

            string timerText = string.Format("Tiempo : {0}", seconds);

            gameTimerText.text = timerText;

            if (countDown < 0)
            {
                scGameManager.instance.stateGame = 4;
            }
        }
        
    }
}
