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
    /*protected virtual void Move()
    {
        // If Enemy didn't reach last waypoint it can move
        // If enemy reached last waypoint then it stops
        if (waypointIndex <= waypoints.Length - 1)
        {
            // Move Enemy from current waypoint to the next one
            // using MoveTowards method
            transform.position = Vector2.MoveTowards(transform.position,
               waypoints[waypointIndex].transform.position,
               speed * Time.deltaTime);

            // If Enemy reaches position of waypoint he walked towards
            // then waypointIndex is increased by 1
            // and Enemy starts to walk to the next waypoint
            if (transform.position == waypoints[waypointIndex].transform.position)
            {
                waypointIndex += 1;
                ChangeRotation();
            }

            if (transform.position.x < waypoints[0].transform.position.x)
            {
                Quaternion target = Quaternion.Euler(0, 180, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            }
            if (transform.position.x > waypoints[1].transform.position.x)
            {
                Quaternion target = Quaternion.Euler(0, 0, 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            }
        }
        //Repeat
        else if (waypointIndex >= waypoints.Length)
        {
            waypointIndex = 0;
            ChangeRotation();
        }
    }*/
    protected virtual void ChangeRotation()
    {
        /*if (waypointIndex == 0)
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            //canon.negative = true;
        }
        else if (waypointIndex == 1)
        {
            Quaternion target = Quaternion.Euler(0, 180, 0);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            //canon.negative = false;
        }*/

        currentSpeed.x *= -1;
        if(currentSpeed.x >= 1)
        {
            //Right
            //Quaternion target = Quaternion.Euler(0, 180, 0);
            //transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            sprite.flipX = true;
        }
        else if (currentSpeed.x <= -1)
        {
            //Left
            //Quaternion target = Quaternion.Euler(0, 0, 0);
            //sprite.transform.rotation = Quaternion.Slerp(transform.rotation, target, 1f);
            sprite.flipX = false;
        }
    }
}
