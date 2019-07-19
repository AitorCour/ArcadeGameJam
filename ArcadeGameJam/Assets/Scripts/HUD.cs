using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public GameObject pauseObj;
    private bool paused;

    private GameObject textObj;
    private Text disText;
    private bool textActive;
    private void Start()
    {
        textObj = GameObject.FindGameObjectWithTag("Text");
        disText = GameObject.FindGameObjectWithTag("Text").GetComponent<Text>();
        paused = false;
        pauseObj.SetActive(false);
        textActive = false;
        textObj.SetActive(false);
    }
    public void Pause()
    {
        if(paused)
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

}
