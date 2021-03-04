using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ShopManager : MonoBehaviour
{
    public class Trait // Unity 상에서 UI 구현을 해놓았으나 필요시 코드상으로 옮길 것
    {
        
        public int price;
        public string name;
        public string desc; // description
        public Trait(string n, string d, int p)
        {
            price = p;
            desc = d;
            name = n;
        }
        public void BuyTrait()
        {
            int money;
            money = PlayerPrefs.GetInt("Money");
            money -= price;
            PlayerPrefs.SetInt("Money", money);
        }
    }
    public Button[] traitButton = new Button[4];
    public Text[] traitText = new Text[4];
    public Text[] buttonText = new Text[4];
    public Text moneyleft;
    public int pagenumber;
    public int maxpage;
    public Button[] pageButton = new Button[2];
    public Text pageText;
    public Trait[] traits;
    // Start is called before the first frame update
    void Start()
    {
        pagenumber = 0;
        traits = new Trait[5]; // 특성 최대갯수 : 4
        traits[0]=new Trait("Adrenaline Junkie", "half time, result score doubles", 1000);
        traits[1] = new Trait("Perfectionist", "Time stops after hint(per round), hitting wrong pair ends game", 1500);
        traits[2] = new Trait("Clown", "shuffling after hitting a pair gives 10s", 500);
        traits[3] = new Trait("False Start", "starts with one more hint", 3000);
        traits[4] = new Trait("Desperate Hints", "unlimited Hints, but hint time halves each time", 1500);
        maxpage = (traits.Length / 4) - 1;  // error might occur if trait length goes 0
        if (traits.Length % 4 != 0) maxpage++;
        pageButton[0].onClick.AddListener(PrevPageButtonClick);
        pageButton[1].onClick.AddListener(NextPageButtonClick);
    }
    void TraitButtonClick(int i)
    {
        if (PlayerPrefs.GetInt("Trait" + i.ToString()) == 0) // 안샀으면
        {
            if (PlayerPrefs.GetInt("Money") >= traits[i].price)
            {
                traits[i].BuyTrait();
                PlayerPrefs.SetInt("Trait" + i.ToString(), 1); // 0: not bought 1: enable 2: disable
                buttonText[i%4].text = "Enabled";
            }
            else
            {
                // not enough money
            }
        }
        else if (PlayerPrefs.GetInt("Trait" + i.ToString()) == 1) // Enabled
        {
            PlayerPrefs.SetInt("Trait" + i.ToString(), 2);
            buttonText[i%4].text = "Disabled";
        }
        else if (PlayerPrefs.GetInt("Trait" + i.ToString()) == 2) //Disabled
        {
            PlayerPrefs.SetInt("Trait" + i.ToString(), 1);
            buttonText[i%4].text = "Enabled";
        }
    }
    void NextPageButtonClick()
    {
        if (pagenumber < maxpage)
        {
            pagenumber++;
        }
        else
        {
            pagenumber = 0;
        }
    }
    void PrevPageButtonClick()
    {
        if (pagenumber > 0)
        {
            pagenumber--;
        }
        else
        {
            pagenumber = maxpage;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.HasKey("Money")) moneyleft.text = "Money : " + PlayerPrefs.GetInt("Money").ToString(); // 시간 표시 텍스트
        pageText.text = (pagenumber + 1).ToString() + " / " + (maxpage+1).ToString();
        for(int i = 0; i < 4; i++) // 페이지당 4개 표시
        {
            int j = i + pagenumber * 4;
            if (j<traits.Length) // if trait is available
            {
                traitText[i].gameObject.SetActive(true);
                traitButton[i].gameObject.SetActive(true);
                traitText[i].text = traits[j].name + " : " + traits[j].desc;
                if (PlayerPrefs.GetInt("Trait"+j) == 0) // if not bought
                {
                    buttonText[i].text = traits[j].price.ToString();
                }
                else if (PlayerPrefs.GetInt("Trait"+j) == 1) // if enabled
                {
                    buttonText[i].text = "Enabled";
                }
                else if (PlayerPrefs.GetInt("Trait"+j) == 2) // if disabled
                {
                    buttonText[i].text = "Disabled";
                }

                int temp = i; // closure problem?
                traitButton[temp].onClick.RemoveAllListeners();
                traitButton[temp].onClick.AddListener(() => TraitButtonClick(j));
            }
            else
            {
                //비활성화
                traitText[i].gameObject.SetActive(false);
                traitButton[i].gameObject.SetActive(false);
            }
        }

    }
}
