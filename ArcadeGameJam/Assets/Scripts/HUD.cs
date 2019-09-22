using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject pauseObj;
    private PlayerBehaviour player;
    private bool paused;

    private GameObject textObj;
    private Text disText;
    private bool textActive;
    private int nHearts;
    private int maxHearts = 5;
    private int b;
    private GameObject[] hearts;
    private GameObject midHeart;
    private GameObject[] containerHearts;
    //private Vector2 xPos;
    private void Start()
    {
        textObj = GameObject.FindGameObjectWithTag("Text");
        disText = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        paused = false;
        pauseObj.SetActive(false);
        textActive = false;
        textObj.SetActive(false);
        hearts = GameObject.FindGameObjectsWithTag("heart");
        midHeart = GameObject.FindGameObjectWithTag("midHeart");
        containerHearts = GameObject.FindGameObjectsWithTag("containerHeart");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        for (int i = 0; i < maxHearts; i++)
        {
            containerHearts[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
        }
        ResetLife();
        Debug.Log("Hearts");
        nHearts = 2;
        HeartManager();
        SetLife(player.iniLife);
    }
    public void Pause()
    {
        if (paused)
        {
            pauseObj.SetActive(false);
            paused = false;
        }
        else
        {
            pauseObj.SetActive(true);
            paused = true;
        }
    }
    public void DisplayText(string newText)
    {
        disText.text = newText;
        textObj.SetActive(true);
        textActive = true;
    }
    public void HideText()
    {
        textObj.SetActive(false);
        textActive = false;
    }
    public void AddContainers()//Suma contenedores de corazones con el power up
    {
        nHearts++;
        HeartManager();
    }
    private void HeartManager()
    {
        for(int i = 0; i < nHearts; i++)
        {
            //xPos = containerHearts[i].anchoredPosition;
            containerHearts[i].GetComponent<RectTransform>().anchoredPosition = new Vector2((i * 150) + 100, -93);
        }
    }
    public void SetLife(int life)
    {

        ResetLife();
        bool isEven = life % 2 == 0;
        if (isEven)
        {
            life /= 2;
            for (int i = 0; i < life; i++)
            {
                hearts[i].GetComponent<RectTransform>().anchoredPosition = new Vector2((i * 150) + 100, -100);
            }
        }
        if(!isEven)
        {
            //Se resta uno y se parte en dos. a la i se le suma uno, y luego se coloca la unica mitad que existe, con la i+1 (b)
            life = (life - 1) / 2;
            for (int j = 0; j < life; j++)
            {
                hearts[j].GetComponent<RectTransform>().anchoredPosition = new Vector2((j * 150) + 100, -100);
                b = j + 1;
            }
            if(player.life == 1)
            {
                b -= 1;
            }
            midHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2((b * 150) + 100, -93);
        }
    }
    private void ResetLife()
    {
        //Llamar cada vez que pierda vida, para recolocarla de 0
        for (int i = 0; i < maxHearts; i++)
        {
            hearts[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
        }
        midHeart.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 100);
    }
}
