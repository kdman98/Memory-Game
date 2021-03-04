using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimerScript : MonoBehaviour
{
    public float timeLeft; // 남은 시간 저장
    public Text timeText;
    public bool timestopper;
    // Start is called before the first frame update
    void Start()
    {
        timestopper = false;
        if (PlayerPrefs.GetInt("Trait0") == 1) timeLeft = PlayerPrefs.GetInt("Time") / 2; // 라운드당 시간 60초로 설정 - 변경 가능
        else timeLeft = PlayerPrefs.GetInt("Time");
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text = "Time : " + ((int)timeLeft).ToString(); // 시간 표시 텍스트
        if(!timestopper) timeLeft -= Time.deltaTime;
        if (timeLeft < 0.0) // 남은 시간이 0보다 작으면 게임오버
        {
            SceneManager.LoadScene("GameOverScene"); 
        }
    }
    public void TimePause()
    {
        timestopper = true;
    }
    public void TimeResume()
    {
        timestopper = false;
    }
    public void ForceTimeOver()
    {
        timeLeft = -1f;
    }
    public float CheckTimeLeft()
    {
        return timeLeft;
    }
}
