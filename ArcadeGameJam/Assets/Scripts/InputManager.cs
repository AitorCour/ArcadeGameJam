using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerBehaviour player;
    private Weapon weapon;
    public GameObject pause;
    private bool paused;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Weapon>();
        pause.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputAxis = Vector2.zero;
        inputAxis.x = Input.GetAxisRaw("Horizontal");
        player.SetAxis(inputAxis);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            player.StartJump();
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            player.StopJump();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if(!player.dead)
            {
                weapon.ShotWeapon();
                player.Shoot();
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (!player.dead)
            {
                Debug.Log("Eating");
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(paused)
            {
                pause.SetActive(false);
                paused = false;
                Time.timeScale = 1;
            }
            else
            {
                pause.SetActive(true);
                paused = true;
                Time.timeScale = 0;
            }
        }
    }
}
