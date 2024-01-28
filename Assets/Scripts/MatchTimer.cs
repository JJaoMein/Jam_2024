using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathTimer : MonoBehaviour
{
    [SerializeField]
    float matchDuration;
    private float currentMatchTime;

    [SerializeField]
    private float countdownMinutes;
    private float countdownSeconds;

    [SerializeField]
    private TMP_Text countdownText;

    // Start is called before the first frame update
    void Awake()
    {
        //currentMatchTime = matchDuration;
        countdownText = GetComponent<TMP_Text>();
        currentMatchTime = countdownMinutes * 60;
    }

    // Update is called once per frame
    void Update()
    {
        SetMatchTimer();
    }

    private void SetMatchTimer()
    {
        //countdownMinutes = Mathf.FloorToInt(currentMatchTime / 60);
        //countdownSeconds = Mathf.FloorToInt(currentMatchTime % 60);
        countdownSeconds = Mathf.FloorToInt(currentMatchTime / countdownMinutes);
        currentMatchTime -= 1 * Time.deltaTime;
        Debug.Log(currentMatchTime);
        //countdownText.text = currentMatchTime.ToString("0");
        countdownText.text = string.Format("{0:00}:{1:00}", countdownMinutes, countdownSeconds);

        if (countdownSeconds == 0 && currentMatchTime != 0)
        {
            countdownMinutes -= 1;
        }
        if (currentMatchTime <= 0)
        {
            currentMatchTime = 0;
            //Winner Winner chicken dinner

        }
    }
}
