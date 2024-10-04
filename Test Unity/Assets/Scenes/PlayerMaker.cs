using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMaker : MonoBehaviour
{

    public float cooldown;
    public float cooldownAmount = 99;
    public float cooldownAverage = 30;
    public GameObject prefab;

    public bool startGame = false;
    public Button startButton;
    public TMP_Text numText;
    public TMP_Text winText;
    public TMP_Text cashText;
    public int cash = 10;
    public int numObj = 0;

    public Color[] colours;
    public Button[] colButtons;
    public int colSelected = -1;

    public Slider betSlider;
    public TMP_Text betText;
    public TMP_Text betAmount;
    public TMP_Text betView;
    public int betVal;
    public float betCooldown = 3;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(cooldownAverage-15,cooldownAverage+15);

		startButton.onClick.AddListener(buttonClicked);
        cashText.text = ""+cash;
        startButton.gameObject.SetActive(false);

        colours = new Color[] { new Color(0, 1, 0), new Color(1, 0, 0), new Color(0, 0, 1) };
        for (var i = 0; i < 3; i++) colButtons[i].GetComponent<Image>().color = new Color(colButtons[i].GetComponent<Image>().color.r,colButtons[i].GetComponent<Image>().color.g,colButtons[i].GetComponent<Image>().color.b,0f);
        colButtons[0].onClick.AddListener(colR);
        colButtons[1].onClick.AddListener(colG);
        colButtons[2].onClick.AddListener(colB);
        colButtons[3].GetComponent<Image>().color = new Color(1,1,1,0);
        betSlider.gameObject.SetActive(false);
        betText.gameObject.SetActive(false);
        betAmount.gameObject.SetActive(false);
        betView.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        numObj = GameObject.FindObjectsOfType(typeof(Player)).Length;
        numText.text = "Balls left: " + numObj;
        if (startGame) {
            if (betCooldown < 3) {
                betCooldown -= Time.deltaTime;
                if (betCooldown <= 0) {
                    //Restart game
                    Destroy(GameObject.FindGameObjectWithTag("Player"));
                    cooldownAmount = 99;
                    startGame = false;
                    winText.text = "";
                    betCooldown = 3;
                    betView.gameObject.SetActive(false);
                    betView.text = "";
                    colButtons[3].GetComponent<Image>().color = new Color(1,1,1,0);
                }
            } else if (numObj <= 1) {
                GameObject obj = GameObject.FindGameObjectWithTag("Player");
                Color objCol = obj.GetComponent<Renderer>().material.color;
                if (((objCol.r == 1) && colSelected == 0) || ((objCol.g == 1) && colSelected == 1) || ((objCol.b == 1) && colSelected == 2)) {
                    winText.text = "Winner!!";
                    cash += betVal * 2;
                    cashText.text = ""+cash;
                } else {
                    winText.text = "You lost...";
                }
                betCooldown -= Time.deltaTime;
            }
        } else {
            if (cooldownAmount > 0) {
                if (cooldown > 0) cooldown--; else {
                    cooldown = Random.Range(cooldownAverage-15,cooldownAverage+15);
                    GameObject obj = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
                    obj.GetComponent<Renderer>().material.color = colours[Random.Range(0,3)];
                    float size = Random.Range(0.8f,1.2f);
                    obj.transform.localScale = new Vector3(size,size,size);
                    obj.GetComponent<Player>().maker = this;
                    cooldownAmount--;
                }
            } else if (cooldownAmount == 0) {
                cooldownAmount--;
                startButton.gameObject.SetActive(true);
                for (var i = 0; i < 3; i++) colButtons[i].GetComponent<Image>().color = new Color(colButtons[i].GetComponent<Image>().color.r,colButtons[i].GetComponent<Image>().color.g,colButtons[i].GetComponent<Image>().color.b,0.3f);
                betSlider.gameObject.SetActive(true);
                betText.gameObject.SetActive(true);
                betAmount.gameObject.SetActive(true);
            } else {
                betVal = ((int)(betSlider.value*cash));
                if (betVal == 0) betVal = 1;
                betAmount.text = ""+betVal;
            }
        }
    }

    void buttonClicked(){
        if ((cooldownAmount == -1) && (colSelected != -1)) {
            startGame = true;
            cooldownAmount = 99;
            startButton.gameObject.SetActive(false);
            colButtons[3].GetComponent<Image>().color = new Color((colSelected == 0) ? 1 : 0,(colSelected == 1) ? 1 : 0,(colSelected == 2) ? 1 : 0,1);
            for (var i = 0; i < 3; i++) colButtons[i].GetComponent<Image>().color = new Color(colButtons[i].GetComponent<Image>().color.r,colButtons[i].GetComponent<Image>().color.g,colButtons[i].GetComponent<Image>().color.b,0f);
            betSlider.gameObject.SetActive(false);
            betText.gameObject.SetActive(false);
            betAmount.gameObject.SetActive(false);
            betView.gameObject.SetActive(true);
            betView.text = "£"+betVal;
            cash -= betVal;
            if (cash < 0) cash = 0;
            cashText.text = ""+cash;
        }
    }

    void colR(){
        if (cooldownAmount == -1) {
            for (var i = 0; i < 3; i++) colButtons[i].GetComponent<Image>().color = new Color(colButtons[i].GetComponent<Image>().color.r,colButtons[i].GetComponent<Image>().color.g,colButtons[i].GetComponent<Image>().color.b,0.3f);
            colButtons[0].GetComponent<Image>().color = new Color(colButtons[0].GetComponent<Image>().color.r,colButtons[0].GetComponent<Image>().color.g,colButtons[0].GetComponent<Image>().color.b,1f);
            colSelected = 0;
        }
    }

    void colG(){
        if (cooldownAmount == -1) {
            for (var i = 0; i < 3; i++) colButtons[i].GetComponent<Image>().color = new Color(colButtons[i].GetComponent<Image>().color.r,colButtons[i].GetComponent<Image>().color.g,colButtons[i].GetComponent<Image>().color.b,0.3f);
            colButtons[1].GetComponent<Image>().color = new Color(colButtons[1].GetComponent<Image>().color.r,colButtons[1].GetComponent<Image>().color.g,colButtons[1].GetComponent<Image>().color.b,1f);
            colSelected = 1;
        }
    }

    void colB(){
        if (cooldownAmount == -1) {
            for (var i = 0; i < 3; i++) colButtons[i].GetComponent<Image>().color = new Color(colButtons[i].GetComponent<Image>().color.r,colButtons[i].GetComponent<Image>().color.g,colButtons[i].GetComponent<Image>().color.b,0.3f);
            colButtons[2].GetComponent<Image>().color = new Color(colButtons[2].GetComponent<Image>().color.r,colButtons[2].GetComponent<Image>().color.g,colButtons[2].GetComponent<Image>().color.b,1f);
            colSelected = 2;
        }
    }
}
