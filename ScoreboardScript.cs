using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreboardScript : MonoBehaviour
{
    //  public Text scoreText;
    public int score; // 점수
    public Text scoreText;
    public TimerScript timer; // 게임 남은 시간 계산용
    public int destroyedpairs; // 맞춘 카드 짝 수
    public int money;
    // Start is called before the first frame update
    void Start()
    {
        destroyedpairs = 0;
        if (PlayerPrefs.GetInt("Stage") == 1)
        {
            score = 0; // 시작 점수 0
        }
        else if (PlayerPrefs.HasKey("Score")) // 레벨 2부터는 점수 로드
        {
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            score = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score : " + score.ToString(); // 점수 화면에 표시
        if (CheckDone()) // all pairs destroyed - one card(prefab) left on Scene
        {
            score += (int)timer.CheckTimeLeft() * 10 * PlayerPrefs.GetInt("Stage");
            PlayerPrefs.SetInt("Score", score); // 점수 저장
            if (PlayerPrefs.GetInt("Stage") == 3)
            {
                SceneManager.LoadScene("GameClear");
            }
            else
            {
                PlayerPrefs.SetInt("Stage", (PlayerPrefs.GetInt("Stage")) + 1); // 다음 스테이지 저장
                SceneManager.LoadScene("AfterStage");
            }
            
        }

    }
    public bool CheckDone()
    {
        GameObject[] tempCard = GameObject.FindGameObjectsWithTag("Card");
        if (tempCard.Length == 1)
        {
            return true;
        }
        else return false;
    }
    public void DestroyedPair() // 한 쌍의 카드 삭제시
    {
        score += 100; // 점수 추가
        destroyedpairs++;
        money = PlayerPrefs.GetInt("Money");
        PlayerPrefs.SetInt("Money", money + 100); // 카드 한 세트 파괴당 100원 획득
        PlayerPrefs.SetInt("Score", score); // 게임 오버를 대비해 저장
    }

    public int CheckScore()
    {
        return score;
    }
}
