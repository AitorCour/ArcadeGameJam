using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerBehaviour player;
    private HUD hud;
    private Weapon weapon;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        hud = GetComponent<HUD>();
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>();
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector2 inputAxis = Vector2.zero;
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        player.SetAxis(inputAxis);*/
        if(Input.GetAxisRaw("Horizontal") > 0 && !paused)
        {
            player.SetAxis(new Vector2(1, 0));
        }
        else if (Input.GetAxisRaw("Horizontal") < 0 && !paused)
        {
            player.SetAxis(new Vector2(-1, 0));
        }
        else player.SetAxis(new Vector2(0, 0));

        if (Input.GetButtonDown("Jump"))
        {
            player.StartJump();
        }
        if(Input.GetButtonUp("Jump"))
        {
            player.StopJump();
        }
        if (Input.GetButtonDown("Fire1") && !paused)
        {
            if(!player.dead || !paused)
            {
                weapon.ShotWeapon();
                player.Shoot();
            }
        }
        if (Input.GetButtonDown("Fire2") && !paused)
        {
            if (!player.dead || !paused)
            {
                Debug.Log("Eating");
                player.Eat();
            }
        }
        if (Input.GetButtonDown("Pause"))
        {
            if(paused)
            {
                hud.Pause();
                paused = false;
                Time.timeScale = 1;
            }
            else
            {
                hud.Pause();
                paused = true;
                Time.timeScale = 0;
            }
        }
    }
}
