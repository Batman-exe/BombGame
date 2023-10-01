using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

    [SerializeField]private float timeRemaining = 300;
    private bool timeIsRunning = true;
    [SerializeField]private TextMeshPro timeText;

    public bool timeIsUp = false;


    private void Awake()
    {
        timeIsRunning = true;
    }


    private void Update()
    {
        if (timeIsRunning)
        {
            if(Mathf.FloorToInt(timeRemaining) >= 2)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
                Debug.Log(Mathf.FloorToInt(timeRemaining));
            } else if (Mathf.FloorToInt(timeRemaining) == 1)
            {
                timeIsUp = true;
                Debug.Log("lose");
            }
        }
    }


    private void DisplayTime(float timeToDisplay)
    {
        timeToDisplay -= 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float second = Mathf.FloorToInt(timeToDisplay % 60);
        if(second == 0)
        {
            timeText.text = string.Format("{0:00} : {1:00}", 0, 0);
        } else
        {
            timeText.text = string.Format("{0:00} : {1:00}", minutes, second);
        }
    }

}
