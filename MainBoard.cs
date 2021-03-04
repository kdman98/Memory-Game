using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//change per stage : boardsize, card spawn, clear
public class MainBoard : MonoBehaviour
{
    public List<int> deck; // board
    int boardRow,boardCol;

    //card
    public SpriteRenderer cardSprite; // 카드 스프라이트 표시
    public Sprite[] cardFace; // 앞면 - unity에서 카드 배열 지정해놓음.
    public GameObject cardObject; // instantiate 할 prefab 지정 - Unity에서 Card로 지정
    public GameObject userInput;
    

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Stage") == 1)
        {
            boardRow = 2;  // 스테이지 1 - 카드 4장
            boardCol = 2;
        }
        else if (PlayerPrefs.GetInt("Stage") == 2)
        {
            boardRow = 2;  // 스테이지 2 - 카드 8장
            boardCol = 4;
        }
        else if (PlayerPrefs.GetInt("Stage") == 3)
        {
            boardRow = 3;  // 스테이지 3 - 카드 18장
            boardCol = 6;
        }
        else
        {
            Debug.Log("Game level load failure. Check if the level name is correct.");
        }
        DeployCards(); // 덱을 생성해서 보드에 배치
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void DeployCards() // 덱 생성 & 보드 구성
    {
        deck = GenerateCards(boardRow*boardCol); //덱 생성
        Shuffle<int>(deck); // 무작위 셔플
        GameObject background = GameObject.Find("background");
        background.transform.localScale = new Vector3(boardCol + 1, boardRow*1.4f + 0.5f,0);
        background.transform.position = new Vector3(0, 2.1f-0.7f*boardRow, -0.03f);
        int cardCol = 0;
        float xOffset = 0.5f; // starting offset - goes left by 0.5f per column
        xOffset -= (float)boardCol / 2;
        float yOffset = 1.4f;
        float zOffset = 0.0f;
        foreach (int card in deck)
        {
            GameObject newcard = Instantiate(cardObject, new Vector3(xOffset,yOffset,zOffset), Quaternion.identity);
            newcard.name = card.ToString(); // 카드 gameobject 생성 후 object name을 카드 앞면 값으로 지정
            newcard.GetComponent<CardScript>().cardFace = cardFace[card]; // 카드 앞면 값에 스프라이트 지정
            xOffset = xOffset + 1.0f;
            cardCol++;
            if (cardCol >= boardCol) // 한 줄을 넘어가면 다음 줄에 배치
            {
                xOffset = 0.5f - ((float)boardCol / 2);
                yOffset = yOffset - 1.4f;
                cardCol = 0;
            }
        }
    }
    public static List<int> GenerateCards(int num) // 덱 랜덤 숫자(0~51) 생성 후 리스트 리턴
    {
        List<int> newDeck = new List<int>();
        int temp;
        if (num % 2 != 0) // 홀수 개의 카드 배치 시도 시 오류 출력
        {
            Debug.Log("CAN'T DEPLOY ODD NUMBER OF CARDS!!!");
        }
        //get random number from cards index, and add to deck if there's no same card.
        for (int i = 0; i < num / 2; i++)// 덱의 카드 범위 안에서, 랜덤하게 카드를 뽑아서 두 장 복사해 넣음
        {

            temp = (Random.Range(0, 52)); // 하트 에이스부터 스페이드 킹까지
            if (!newDeck.Contains(temp)) // 중복된 카드를 뽑으면 넣지 않음
            {
                newDeck.Add(temp); //뽑은 카드를 두 장 덱에 추가
                newDeck.Add(temp);
            }
            else i--;
        }

        return newDeck; // 완성된 덱 리턴
    }
    void Shuffle<T>(List<T> list) // 리스트에 대한 무작위 셔플
    {
        System.Random random = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            int k = random.Next(n);
            n--;
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }

}
