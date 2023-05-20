using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerToFinish : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject levelFailedUI;
    [SerializeField] int min;
    [SerializeField] int sec;
    [SerializeField] float startTime;
    public bool levelCompleted = false;
    [SerializeField] bool levelFailed = false;
    
   
    void Update()
    {
        if (startTime <= 0f || levelCompleted || levelFailed)
            return;

        startTime -= Time.deltaTime;
        sec = (int)(startTime % 60);
        min = (int)(startTime / 60 % 60);

        timerText.text = string.Format("{0:00}:{1:00}", min, sec);

        if(startTime <= 0f)
        {
            levelFailed = true;
            LevelFailed();
        }
            
    }

    void LevelFailed()
    {
        if (!GameManager.instance.ball.dead)
        {
            GameManager.instance.ball.Dead();
            if (levelFailedUI != null)
                levelFailedUI.SetActive(true);
        }

    }
}
