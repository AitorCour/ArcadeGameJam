using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    protected PlayerBehaviour playerBe;
    private EnemyHead head;

    protected bool isDead;
    protected bool playerDetected;
    public bool canDoDamage;
    protected int enemyLife;

    public Transform[] waypoints;
    public int waypointIndex = 0;
    protected float speed;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerBe = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
        head = GetComponentInChildren<EnemyHead>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        //Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canDoDamage)
        {
            playerBe.Damage(1);
        }
    }
    public void Damage(int damage)
    {
        enemyLife -= damage;
        if (enemyLife <= 0)
        {
            Death();
            enemyLife = 0;
        }
    }
    void Death()
    {
        isDead = true;
        this.enabled = false;
        head.enabled = false;
        transform.position = new Vector2(-20, -20);
    }
    protected virtual void Move()
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
    }
    protected virtual void ChangeRotation()
    {
        if (waypointIndex == 0)
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
        }
    }
}
