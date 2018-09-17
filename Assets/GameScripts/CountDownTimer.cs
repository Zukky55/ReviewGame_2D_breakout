using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>ゲームに制限時間を設定する/// </summary>
public class CountDownTimer : MonoBehaviour
{
    // Total time limit
    float totalTime;
    // Time limit (minute)
    int minute = 0;
    // Time limit (seconds)
    float seconds = 30;
    // Number of seconds since the last update
    private float oldSeconds;
    private Text timerText;
    //inspector上でtimeLimitを設定するメンバ変数
	public int min;
	public float sec;
    // GameController用
    GameObject gc;

    public void Start()
    {
    }

    public void Update()
    {
        gc = GameObject.Find("GameController");
        GameController d1 = gc.GetComponent<GameController>();
        timerText = GetComponentInChildren<Text>();

        if (d1.flag == true)
        {
            // time limitを初期値に戻す
            minute = min;
            seconds = sec;
            totalTime = minute * 60 + seconds;
            oldSeconds = 0f;
            timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            d1.flag = false;
        }

        if (d1.m_isInGame)
        {


            // If the time limit is less than 0 seconds, do not do anything
            if (totalTime <= 0f)
            {
                return;
            }

            // Once measuring the total time limit
            totalTime = minute * 60 + seconds;
            totalTime -= Time.deltaTime;

            //Resetting
            minute = (int)totalTime / 60;
            seconds = totalTime - minute * 60;

            // タイマー表示用UIテキストに時間を表示する
            if ((int)seconds != (int)oldSeconds)
            {
                timerText.text = minute.ToString("00") + ":" + ((int)seconds).ToString("00");
            }
            oldSeconds = seconds;

            // 制限時間以下になったらGameControllerClass EndGame Methodを呼び出す
            if (totalTime <= 0f)
            {
                d1.EndGame();
            }
        }
    }
}