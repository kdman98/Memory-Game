 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardScript : MonoBehaviour
{
    public SpriteRenderer cardSprite;
    public Sprite cardFace; // 앞면
    public Sprite cardTail; // 뒷면
    public bool faceUp; // 0 if Tail , 다른 클래스에서 호출 가능성 있음

    // Start is called before the first frame update
    void Start()
    {
        faceUp = false; // 시작은 뒷면
       

        cardSprite = GetComponent<SpriteRenderer>();
        ShowCardFace(faceUp);

    }
    public void ShowCardFace(bool flipped) // flipped = true면 앞면, else 뒷면 - 을 sprite에 저장
    {
        if (flipped)
        {
            cardSprite.sprite = cardFace;
        }
        else
        {
            cardSprite.sprite = cardTail;
        }
    }
   
    // Update is called once per frame
    void Update()
    {
        ShowCardFace(faceUp);
    } 
}