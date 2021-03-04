using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScore : MonoBehaviour
{
    public int score;
    public Text scoreText;
    public Text rank1, rank2, rank3;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Score")) // Score 저장되어있으면 불러오기
        {
            score = PlayerPrefs.GetInt("Score");
            if (PlayerPrefs.GetInt("Trait0") == 1)
            {
                score *= 2;
            }
        }
        else // 저장된 점수가 없으면 0
        {
            score = 0;
        }
        scoreText.text = "Final Score : " + score;
        SearchRank(score); // 최고 기록 갱신
        if (PlayerPrefs.HasKey("Rank1")) // 최고 기록 표시
        {
            rank1.text = "1st : " + (PlayerPrefs.GetInt("Rank1").ToString());
        }
        if (PlayerPrefs.HasKey("Rank2"))
        {
            rank2.text = "2nd : " + (PlayerPrefs.GetInt("Rank2").ToString());
        }
        if (PlayerPrefs.HasKey("Rank3"))
        {
            rank3.text = "3rd : " + (PlayerPrefs.GetInt("Rank3").ToString());
        }
    }

    // Update is called once per frame
    void SearchRank(int score) // 최고 기록 갱신
    {
        if (!PlayerPrefs.HasKey("Rank1"))
        {
            PlayerPrefs.SetInt("Rank1", score);
        }
        else if (PlayerPrefs.GetInt("Rank1") < score)
        {
            PlayerPrefs.SetInt("Rank3", PlayerPrefs.GetInt("Rank2"));
            PlayerPrefs.SetInt("Rank2", PlayerPrefs.GetInt("Rank1"));
            PlayerPrefs.SetInt("Rank1", score);
        }
        else if (!PlayerPrefs.HasKey("Rank2"))
        {
            PlayerPrefs.SetInt("Rank2", score);
        }
        else if (PlayerPrefs.GetInt("Rank2") < score)
        {
            PlayerPrefs.SetInt("Rank3", PlayerPrefs.GetInt("Rank2"));
            PlayerPrefs.SetInt("Rank2", score);
        }
        else if (!PlayerPrefs.HasKey("Rank3"))
        {
            PlayerPrefs.SetInt("Rank3", score);
        }
        else if (PlayerPrefs.GetInt("Rank3") < score)
        {
            PlayerPrefs.SetInt("Rank3", score);
        }
    }
    void Update()
    {
        
    }

    public int CheckScore()
    {
        return score;
    }
}
