using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class MathTimer : MonoBehaviour
{
    [SerializeField]
    float matchDuration;

    [SerializeField]
    public UnityEvent GameOver;

    private float currentMatchTime;
    private float countdownMinutes;
    private float countdownSeconds;

    private TMP_Text countdownText;

    private bool gameFinished;

    void Awake()
    {
        gameFinished = false;
        //countdownText = GetComponent<TMP_Text>();
        currentMatchTime = matchDuration * 60;
    }

    void Update()
    {
        SetMatchTimer();
    }

    private void SetMatchTimer()
    {
        if (gameFinished == false)
        {
            if (currentMatchTime >= 0)
            {
                countdownMinutes = Mathf.FloorToInt(currentMatchTime / 60);
                countdownSeconds = Mathf.FloorToInt(currentMatchTime % 60);
                currentMatchTime -= 1 * Time.deltaTime;
                //Debug.Log(currentMatchTime);
                //countdownText.text = string.Format("{0:00}:{1:00}", countdownMinutes, countdownSeconds);
            }
            else
            {
                currentMatchTime = 0;
                //Winner Winner chicken dinner
                gameFinished = true;
                GameOver?.Invoke();
                Time.timeScale = 0;

            }
        }
    }
}
