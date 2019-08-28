using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerBehaviour player;
    private HUD hud;
    // Start is called before the first frame update
    /*private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        hud = GetComponent<HUD>();
        player.life = player.iniLife;
    }
    void Start()
    {
        hud.SetLife(player.life);
    }*/

}
