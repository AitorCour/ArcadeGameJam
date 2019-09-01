using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    //Power up speed
    /*public bool speed;
    public bool jump;*/
    public bool tripleCannon;
    public bool bounce;
    public bool life;
    public bool size;
    public bool damage;

    public Sprite triplePrefab;
    public Sprite bouncePrefab;
    public Sprite lifePrefab;
    public Sprite sizePrefab;
    public Sprite damagePrefab;

    private bool isInsideTrigger = false;
    private PlayerBehaviour player;
    private PowerManager pManager;
    private SpriteRenderer sRenderer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        pManager = GetComponentInParent<PowerManager>();
        sRenderer = GetComponentInChildren<SpriteRenderer>();

        int value = Random.Range(0, 5);
        Debug.Log(value);

        if(value == 0)
        {
            size = true;
            sRenderer.sprite = sizePrefab;
        }
        else if(value == 1)
        {
            damage = true;
            sRenderer.sprite = damagePrefab;
        }
        else if(value == 2)
        {
            tripleCannon = true;
            sRenderer.sprite = triplePrefab;
        }
        else if (value == 3)
        {
            bounce = true;
            sRenderer.sprite = bouncePrefab;
        }
        else if (value == 4)
        {
            life = true;
            sRenderer.sprite = lifePrefab;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isInsideTrigger)
        {
            if(Input.GetKeyDown(KeyCode.Return))
            {
                /*if(speed)
                {
                    player.speed += 3;
                }*/
                /*else if(jump)
                {
                    player.jumpForce += 12;
                }*/
                if (size)
                {
                    player.bulletsScale *= 1.2f;
                    player.BulletsScale();
                }
                else if(damage)
                {

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
                    player.GetLife();
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
