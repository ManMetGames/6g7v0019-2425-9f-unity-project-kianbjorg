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

    // Start is called before the first frame update
    void Start()
    {
        cooldown = Random.Range(cooldownAverage-15,cooldownAverage+15);

		startButton.onClick.AddListener(buttonClicked);
        cashText.text = ""+cash;
        startButton.GetComponent<Image>().color = new Color(1,1,1);

        colours = new Color[] { new Color(0, 1, 0), new Color(1, 0, 0), new Color(0, 0, 1) };
    }

    // Update is called once per frame
    void Update()
    {
        numObj = GameObject.FindObjectsOfType(typeof(Player)).Length;
        numText.text = "Balls left: " + numObj;
        if (startGame) {
            if (numObj <= 1) winText.gameObject.SetActive(true);
        } else {
            if (cooldownAmount > 0) {
                if (cooldown > 0) cooldown--; else {
                    cooldown = Random.Range(cooldownAverage-15,cooldownAverage+15);
                    GameObject obj = Instantiate(prefab, new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
                    obj.GetComponent<Renderer>().material.color = colours[0];
                    obj.GetComponent<Player>().maker = this;
                    cooldownAmount--;
                }
            } else {
                startButton.GetComponent<Image>().color = new Color(0,1,0);
            }
        }
    }

    void buttonClicked(){
        if (cooldownAmount == 0) {
            startGame = true;
            startButton.GetComponent<Image>().color = new Color(1,1,1);
        }
    }
}
