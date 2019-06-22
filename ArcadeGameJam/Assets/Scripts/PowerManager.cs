using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    private GameObject[] powerUps;
    // Start is called before the first frame update
    void Start()
    {
        powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
    }

    // Update is called once per frame
    public void DesactiveAll()
    {
        for(int i = 0; i < powerUps.Length; i++)
        {
            powerUps[i].SetActive(false);
        }
    }
}
