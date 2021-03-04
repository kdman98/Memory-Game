using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInput : MonoBehaviour
{
    public GameObject select1; // selected card slot
    public GameObject select2; // secondary selected card
    // if select1 == select2 destroy both and score++
    private ScoreboardScript scoreboard;
    float timer; // timer to count 1 second after two cards pick
    public int cardselectionflag; // value that how many card is faceUP
    public AudioSource cardopensound; // card open sound
    public AudioSource cardshovesound; // card destroy sound
    public Button shuffleButton;
    public Button hintButton;

    public int hintflag;
    public int hintused;
    public int shuffleflag;
    public int shuffleused;
    public int hintlimit;
    public float hintshowtime;
    // Start is called before the first frame update
    void Start()
    {
        scoreboard = FindObjectOfType<ScoreboardScript>(); // create scoreboard
        cardselectionflag = 0;
        if (PlayerPrefs.HasKey("Hints"))
        {
            hintlimit = PlayerPrefs.GetInt("Hints");
        }
        else
        {
            hintlimit = 1;
        }
        if (PlayerPrefs.GetInt("Trait3") == 1) hintlimit++;
        hintflag = 0;
        hintused = 0;
        hintshowtime = 1.0f;
        shuffleflag = 0;
        shuffleused = 0;
        timer = 0.0f;

        shuffleButton.onClick.AddListener(ShuffleBoard);
        hintButton.onClick.AddListener(Hint);
    }

    // Update is called once per frame
    void Update()
    {

        if(cardselectionflag!=2 && hintflag == 0) GetMouseClick(); // continuously get input when less than 2 card is up
        if (cardselectionflag == 2) // if two card is up
        {
            timer += Time.deltaTime; // timer activates
            if (timer > 1.0f) // after 1 second
            {
                CompareTwoCards(select1, select2); // Compare two cards
                timer = 0.0f; // and reset timer
            }
        }
        else if (hintflag == 1)
        {
            timer += Time.deltaTime; // timer activates
            if (timer > hintshowtime) // after 1 second
            {
                EndHint();
                if (PlayerPrefs.GetInt("Trait4") == 1)
                {
                    hintshowtime /= 2;
                    hintused--;
                }
                timer = 0.0f; // and reset timer
            }
        }
    }
    void GetMouseClick() // get input / change if added keyboard action?
    {
        // NOTICE : one of the following will be executed. via if/else. if/if when needed.
        if (Input.GetMouseButtonDown(0)) // if left clicked, get gameObject which is hit
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -10));
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                if (hit.collider.CompareTag("Card")) // Click on Card
                {
                    Card(hit.collider.gameObject);
                }
                else
                {

                }
            }
        }
        else if (Input.GetMouseButtonDown(1)) // right click
        {
            if (cardselectionflag == 0) Hint();
        }
        else if (Input.GetKeyUp(KeyCode.Alpha1)){ // numkey 1
            ShuffleBoard();
        }
    }
    void ShuffleBoard()
    {
        Vector3 temp;
        GameObject[] tempCard = GameObject.FindGameObjectsWithTag("Card");
        if (tempCard != null)
        {
            cardshovesound.Play();

            System.Random random = new System.Random();
            for (int j = 1; j < tempCard.Length; j++) // card prefab is card[0]
            {
                int k = random.Next(1, tempCard.Length);
                temp = tempCard[k].transform.position;
                tempCard[k].transform.position = tempCard[j].transform.position;
                tempCard[j].transform.position = temp;
            }
        }
        if (PlayerPrefs.GetInt("Trait2") == 1 && scoreboard.destroyedpairs-shuffleflag>0)
        {
            scoreboard.timer.timeLeft += 10.0f;
            shuffleflag = scoreboard.destroyedpairs;
        }
        shuffleused++;
    }
    void Hint()
    {
        if (hintlimit > hintused)
        {
            GameObject[] tempCard = GameObject.FindGameObjectsWithTag("Card");

            if (tempCard != null)
            {
                for (int j = 1; j < tempCard.Length; j++) // card prefab is card[0]
                {
                    tempCard[j].GetComponent<CardScript>().faceUp = true;
                }
                hintflag = 1;
                hintused++;
            }
        }
        else
        {
            Debug.Log("All hints used");
        }
    }
    void EndHint()
    {
        GameObject[] tempCard = GameObject.FindGameObjectsWithTag("Card");
        if (tempCard != null)
        {
            for (int j = 0; j < tempCard.Length; j++)
            {
                tempCard[j].GetComponent<CardScript>().faceUp = false;
            }
            hintflag = 0;
        }
        if (PlayerPrefs.GetInt("Trait1") == 1)
        {
            scoreboard.timer.TimePause();
        }
    }
    void Card(GameObject selected)
    {
       
        cardopensound.Play(); // Card open sound play
        
        // if the card is face up
            // flip it again to face down
        if (selected.GetComponent<CardScript>().faceUp)
        {
            selected.GetComponent<CardScript>().faceUp = false;
            cardselectionflag = 0;
        }
        // if the card is face down
             // if no card is up
                 //flip it
        else if (cardselectionflag==0) 
        {
            selected.GetComponent<CardScript>().faceUp = true;
            select1 = selected;
            cardselectionflag = 1;
        }
             // if a card is up
                //compare two cards after 1 second
        else if (cardselectionflag==1)
        {
            selected.GetComponent<CardScript>().faceUp = true;
            select2 = selected;
            cardselectionflag = 2;
            
        }
    }
    void CompareTwoCards(GameObject select1,GameObject select2) // func for delay needed
    {
        //if two cards are same - destroy / score ++
        if(select1.name == select2.name)
        {
            cardshovesound.Play();
            Destroy(select1);
            Destroy(select2);
            scoreboard.DestroyedPair();
        }
        else //if not same - flip it back
        {
            select1.GetComponent<CardScript>().faceUp = false;
            select2.GetComponent<CardScript>().faceUp = false;
            if(PlayerPrefs.GetInt("Trait1")==1 && hintused > 0) // Perfectionist Fail
            {
                scoreboard.timer.ForceTimeOver();
            }
        }
        

        cardselectionflag = 0; // card is not selected after comparing
    }
}
