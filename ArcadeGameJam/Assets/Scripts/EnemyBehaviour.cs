using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected PlayerBehaviour playerBe;
    protected Animator anim;

    public bool isDead;
    public bool playerDetected;
    public bool playerSeen;
    public bool canDoDamage;
    private bool isRunning;
    public bool canMove;
    private float runDeathCounter;
    protected int enemyLife;

    public Transform[] waypoints;
    public int waypointIndex = 0;
    protected float speed;
    protected Vector2 currentSpeed;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerBe = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        canMove = true;
        currentSpeed.x = 1;
        Debug.Log(currentSpeed);
        isRunning = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!canMove) return;
        //Move();
        if(isRunning == true)
        {
            
            Move();
            if (runDeathCounter >= 2)
            {
                currentSpeed.x = 0;
            }
            if (runDeathCounter >= 2.3f)
            {
                isRunning = false;
                Death();
            }
            else runDeathCounter += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerBe.Damage(1);
            Debug.Log("Collided");
        }
    }
    public void Damage(int damage)
    {
        Debug.Log("DamageDeath");
        enemyLife -= damage;
        if (enemyLife <= 0)
        {
            NoLife();
            enemyLife = 0;
        }
    }
    protected virtual void NoLife()
    {
        Debug.Log("DeathComplete");
    }
    public void Death()
    {
        isDead = true;
        //anim.SetTrigger("Death");
        Debug.Log("DEAD");
        transform.position = new Vector2(-50, -50);
        this.enabled = false;
        anim.enabled = false;
    }
    protected virtual void RunDeath()
    {
        isRunning = true;
        currentSpeed.x = 5;
        Debug.Log(currentSpeed);

        runDeathCounter = 0;
        anim.SetTrigger("Death");
    }
    protected virtual void Move()
    {
        transform.Translate(currentSpeed * Time.deltaTime, Space.World);
    }
    protected virtual void ChangeRotation()
    {

        currentSpeed.x *= -1;
        if(currentSpeed.x >= 1)
        {
            sprite.flipX = true;
        }
        else if (currentSpeed.x <= -1)
        {
            sprite.flipX = false;
        }
    }
}
