using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Money")) // 돈 초기설정
        {
            PlayerPrefs.SetInt("Money", 0);
        }
        if (!PlayerPrefs.HasKey("Time")) // time 초기설정
        {
            PlayerPrefs.SetInt("Time", 60);
        }
        if (!PlayerPrefs.HasKey("Trait0"))
        {
            PlayerPrefs.SetInt("Trait0", 0);
        }
        if (!PlayerPrefs.HasKey("Trait1")) 
        {
            PlayerPrefs.SetInt("Trait1", 0);
        }
        if (!PlayerPrefs.HasKey("Trait2"))
        {
            PlayerPrefs.SetInt("Trait2", 0);
        }
        if (!PlayerPrefs.HasKey("Trait3"))
        {
            PlayerPrefs.SetInt("Trait3", 0);
        }
        if (!PlayerPrefs.HasKey("Trait4"))
        {
            PlayerPrefs.SetInt("Trait4", 0);
        }
        PlayerPrefs.SetInt("Hint", 1);
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("Stage", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
