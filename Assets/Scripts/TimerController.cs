using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;
    IEnumerator coroutine;

    public Text timeCounter;

    private TimeSpan timePlaying;
    private bool timerGoing;

    private float elapsedTime;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        timeCounter.text = "Time: 01:00.00";
        timerGoing = false;
        BeginTimer();
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 90f;
        if(SceneManager.GetActiveScene().name == "Level 1") {
            elapsedTime = 180f;
        }
        coroutine = UpdateTimer();
        StartCoroutine(coroutine);
    }

    public void EndTimer()
    {
        timerGoing = false;
        timeCounter.text = "Time: 00:00.00";
        StopCoroutine(coroutine);
        SceneManager.LoadScene("Game Over");
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime -= Time.deltaTime;
            if (elapsedTime <= 0f) {
                EndTimer();
                yield return null;
            }
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
            timeCounter.text = timePlayingStr;

            yield return null;
        }
    }
}
