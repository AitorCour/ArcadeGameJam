using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    //Power up speed
    public bool speed;
    public bool jump;
    public bool tripleCannon;
    public bool bounce;
    public bool life;

    private bool isInsideTrigger = false;
    private PlayerBehaviour player;
    private PowerManager pManager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        pManager = GetComponentInParent<PowerManager>();
        int value = Random.Range(0, 5);
        Debug.Log(value);
        if(value == 0)
        {
            speed = true;
        }
        else if(value == 1)
        {
            jump = true;
        }
        else if(value == 2)
        {
            tripleCannon = true;
        }
        else if (value == 3)
        {
            bounce = true;
        }
        else if (value == 4)
        {
            life = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isInsideTrigger)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                if(speed)
                {
                    player.speed += 3;
                }
                else if(jump)
                {
                    player.jumpForce += 12;
                }
                else if (tripleCannon)
                {
                    player.cannonsNumber *= 2;
                    player.CannonType();
                }
                else if (bounce)
                {
                    player.bulletBounces *= 2;
                    player.BounceBullet();
                }
                else if (life)
                {
                    player.life += 1;
                }
                pManager.DesactiveAll();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isInsideTrigger = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isInsideTrigger = false;
        }
    }
}
