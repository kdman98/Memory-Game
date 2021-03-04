using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void GameStart()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void ToShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void toGame()
    {
        SceneManager.LoadScene("GameLevel");
    }
    public void toMainLobby()
    {
        SceneManager.LoadScene("MainLobby");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
